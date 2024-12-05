using Microsoft.EntityFrameworkCore;
using Storage.Core.Enitities;
using Storage.DataAccess.Persistence.Migrations;

namespace Storage.DataAccess.Repositories.Impl;

public class ProductionFacilityRepository : IProductionFacilityRepository
{
    private readonly DatabaseContext _context;

    public ProductionFacilityRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<ProductionFacility> GetById(int facilityId)
    {
        var facility = await _context.ProductionFacilities
            .Where(f => f.Code == facilityId)
            .FirstOrDefaultAsync();

        return facility;
    }
}