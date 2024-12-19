using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Storage.Application.Exceptions;
using Storage.Application.Models;
using Storage.Core.Enitities;
using Storage.DataAccess.Persistence.Migrations;

namespace Storage.Application.Services.Impl;

public class ProductionFacilityService : IProductionFacilityService
{
    private readonly ILogger<ProductionFacilityService> _logger;
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public ProductionFacilityService(
        ILogger<ProductionFacilityService> logger,
        DatabaseContext context,
        IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<ProductionFacilityResponseModel> GetByIdAsync(int id)
    {
        var facility = await _context.ProductionFacilities
            .Where(f => f.Code == id)
            .FirstOrDefaultAsync();

        if (facility is null)
        {
            _logger.LogError("Production facility with Code {Code} not found", id);
            throw new EntityNotFoundException();
        }
        
        var response = _mapper.Map<ProductionFacilityResponseModel>(facility);
        
        return response;
    }
}