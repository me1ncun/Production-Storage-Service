using Storage.Application.Models;
using Storage.Core.Enitities;

namespace Storage.Application.Services;

public interface IEquipmentService
{
    Task<EquipmentResponseModel> GetByIdAsync(int equipmentId);
}