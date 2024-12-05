using Microsoft.EntityFrameworkCore;
using Storage.Core.Enitities;
using Storage.DataAccess.Persistence.Migrations;

namespace Storage.DataAccess.Repositories.Impl;

public class EquipmentRepository : IEquipmentRepository
{
    private readonly DatabaseContext _context;

    public EquipmentRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Equipment> GetByIdAsync(int equipmentId)
    {
        var equipment = await _context.Equipments
            .Where(x => x.Code == equipmentId)
            .FirstOrDefaultAsync();
        
        return equipment;
    }
}
