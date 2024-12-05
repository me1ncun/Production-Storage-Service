using Storage.Core.Enitities;

namespace Storage.DataAccess.Repositories;

public interface IEquipmentRepository
{
    Task<Equipment> GetByIdAsync(int equipmentId);
}