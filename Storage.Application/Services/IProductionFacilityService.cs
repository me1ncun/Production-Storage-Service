using Storage.Application.Models;
using Storage.Core.Enitities;

namespace Storage.Application.Services;

public interface IProductionFacilityService
{
    Task<ProductionFacilityResponseModel> GetByIdAsync(int facilityId);
}