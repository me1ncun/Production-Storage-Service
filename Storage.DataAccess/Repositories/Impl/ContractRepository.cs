using Microsoft.EntityFrameworkCore;
using Storage.Core.Enitities;
using Storage.DataAccess.Persistence.Migrations;

namespace Storage.DataAccess.Repositories.Impl;

public class ContractRepository : IContractRepository
{
    private readonly DatabaseContext _context;

    public ContractRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Contract contract)
    {
        await _context.Contracts.AddAsync(contract);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Contract>> GetAllAsync()
    {
        return await _context.Contracts
            .Include(c => c.ProductionFacility)
            .Include(c => c.Equipment)
            .ToListAsync();
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
        
        return usedArea;
    }
}