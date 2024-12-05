using Storage.Application.Models;

namespace Storage.Application.Services;

public interface IContractService
{
    Task<IEnumerable<ContractResponseModel>> GetAllAsync();
    Task CreateAsync(CreateContractModel createContractModel);
}