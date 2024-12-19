using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Storage.Application.Exceptions;
using Storage.Application.Models;
using Storage.Application.Services.Background;
using Storage.Application.Services.Background.Interfaces;
using Storage.Core.Enitities;
using Storage.DataAccess.Persistence.Migrations;

namespace Storage.Application.Services.Impl;

public class ContractService : IContractService
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<ContractService> _logger;
    private readonly IEquipmentService _equipmentService;
    private readonly IProductionFacilityService _productionFacilityService;
    private readonly ILoggingQueue _loggingQueue;

    public ContractService(
        IMapper mapper,
        ILogger<ContractService> logger,
        DatabaseContext context,
        IEquipmentService equipmentService,
        IProductionFacilityService productionFacilityService,
        ILoggingQueue loggingQueue
    )
    {
        _mapper = mapper;
        _logger = logger;
        _context = context;
        _equipmentService = equipmentService;
        _productionFacilityService = productionFacilityService;
        _loggingQueue = loggingQueue;
    }

    public async Task<IEnumerable<ContractResponseModel>> GetAllAsync()
    {
        var contracts = await _context.Contracts
            .Include(c => c.ProductionFacility)
            .Include(c => c.Equipment)
            .ToListAsync();

        var result = _mapper.Map<IEnumerable<ContractResponseModel>>(contracts);

        _logger.LogInformation("Contracts list retrieved successfully");

        return result;
    }

    public async Task CreateAsync(CreateContractModel createContractModel)
    {
        var facility = await _productionFacilityService.GetByIdAsync(createContractModel.FacilityCode);
        
        var equipment = await _equipmentService.GetByIdAsync(createContractModel.EquipmentCode);
        
        var usedArea = await CalculateUsedAreaAsync(facility.Id);

        var requiredArea = equipment.Area * createContractModel.EquipmentQuantity;
        if (usedArea + requiredArea > facility.StandartArea)
        {
            _logger.LogWarning("Insufficient space for equipment in facility {FacilityCode}", facility.Id);
            throw new InsufficientSpaceException();
        }

        var contract = _mapper.Map<Contract>(createContractModel);
        
        await _context.Contracts.AddAsync(contract);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Created contract successfully");

        // Asynchronous background task
        _loggingQueue.Enqueue(contract);
    }

    public async Task<float> CalculateUsedAreaAsync(int facilityId)
    {
        var usedArea = await _context.Contracts
            .Where(c => c.FacilityId == facilityId)
            .Join(_context.Equipments,
                contract => contract.EquipmentId,
                equipment => equipment.Code,
                (contract, equipment) => new
                {
                    Area = equipment.Area * contract.Quantity
                })
            .SumAsync(x => x.Area);

        if (usedArea < 0)
        {
            throw new Exception("Used area cannot be negative");
        }

        return usedArea;
    }
}