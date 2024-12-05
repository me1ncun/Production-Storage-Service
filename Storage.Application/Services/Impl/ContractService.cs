using AutoMapper;
using Microsoft.Extensions.Logging;
using Storage.Application.Exceptions;
using Storage.Application.Models;
using Storage.Core.Enitities;
using Storage.DataAccess.Repositories;

namespace Storage.Application.Services.Impl;

public class ContractService : IContractService
{
    private readonly IContractRepository _contractRepository;
    private readonly IProductionFacilityRepository _productionFacilityRepository;
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ContractService> _logger;

    public ContractService(
        IContractRepository contractRepository,
        IMapper mapper,
        ILogger<ContractService> logger,
        IProductionFacilityRepository productionFacilityRepository,
        IEquipmentRepository equipmentRepository)
    {
        _contractRepository = contractRepository;
        _mapper = mapper;
        _logger = logger;
        _productionFacilityRepository = productionFacilityRepository;
        _equipmentRepository = equipmentRepository;
    }
    
    public async Task<IEnumerable<ContractResponseModel>> GetAllAsync()
    {
        var contracts = await _contractRepository.GetAllAsync();
        
        var result = _mapper.Map<IEnumerable<ContractResponseModel>>(contracts);
        
        _logger.LogInformation("Contracts list retrieved successfully");
        
        return result;
    }

    public async Task CreateAsync(CreateContractModel createContractModel)
    {
        var facility = await _productionFacilityRepository.GetById(createContractModel.FacilityCode);
        if (facility is null)
        {
            _logger.LogError("Production facility with Code {Code} not found", createContractModel.FacilityCode);
            throw new EntityNotFoundException("Production facility not found");
        }
        
        var usedArea  = await _contractRepository.CalculateUsedAreaAsync(facility.Code);
        
        var equipment = await _equipmentRepository.GetByIdAsync(createContractModel.EquipmentCode);
        if (equipment is null)
        {
            _logger.LogError("Equipment with Code {Code} not found", createContractModel.EquipmentCode);
            throw new EntityNotFoundException("Equipment not found");
        }

        var requiredArea = equipment.Area * createContractModel.EquipmentQuantity;
        if (usedArea + requiredArea > facility.StandartArea)
        {
            _logger.LogWarning("Insufficient space for equipment in facility {FacilityCode}", facility.Code);
            throw new InsufficientSpaceException();
        }
        
        var contract = _mapper.Map<Contract>(createContractModel);
        await _contractRepository.AddAsync(contract);
        
        _logger.LogInformation("Created contract successfully");
        
        // Asynchronous background task
        _ = Task.Run(async () =>
        {
            await Task.Delay(1000); 
            
            _logger.LogInformation("Background task executed for contract: {ContractId}", contract.Id);
        });
    }
}