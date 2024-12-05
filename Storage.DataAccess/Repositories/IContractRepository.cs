using Storage.Core.Enitities;

namespace Storage.DataAccess.Repositories;

public interface IContractRepository
{
    Task AddAsync(Contract contract);
    Task<IEnumerable<Contract>> GetAllAsync();
    Task<float> CalculateUsedAreaAsync(int facilityId);
}