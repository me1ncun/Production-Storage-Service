using Storage.Core.Enitities;

namespace Storage.DataAccess.Repositories;

public interface IProductionFacilityRepository
{
    Task<ProductionFacility> GetById(int facilityId);
}