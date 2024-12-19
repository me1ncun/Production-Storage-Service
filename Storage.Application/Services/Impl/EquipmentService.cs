using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Storage.Application.Exceptions;
using Storage.Application.Models;
using Storage.Core.Enitities;
using Storage.DataAccess.Persistence.Migrations;

namespace Storage.Application.Services.Impl;

public class EquipmentService : IEquipmentService
{
    private readonly ILogger<EquipmentService> _logger;
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public EquipmentService(
        ILogger<EquipmentService> logger,
        DatabaseContext context,
        IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<EquipmentResponseModel> GetByIdAsync(int equipmentId)
    {
        var equipment = await _context.Equipments
            .Where(x => x.Code == equipmentId)
            .FirstOrDefaultAsync();

        if (equipment is null)
        {
            _logger.LogError("Equipment with Code {Code} not found", equipmentId);
            
            throw new EntityNotFoundException();
        }
        
        var response = _mapper.Map<EquipmentResponseModel>(equipment);
        
        return response;
    }
}