using AutoMapper;
using Storage.Application.Models;
using Storage.Core.Enitities;

namespace Storage.Application.MappingProfiles;

public class ContractProfile : Profile
{
    public ContractProfile()
    {
        CreateMap<CreateContractModel, Contract>()
            .ForMember(contract => contract.FacilityId, opt => opt.MapFrom(src => src.FacilityCode))
            .ForMember(contract => contract.EquipmentId, opt => opt.MapFrom(src => src.EquipmentCode))
            .ForMember(contract => contract.Quantity, opt => opt.MapFrom(src => src.EquipmentQuantity));
        
        CreateMap<ContractResponseModel, Contract>()
            .ForPath(contract => contract.ProductionFacility.Name, opt => opt.MapFrom(src => src.FacilityName))
            .ForPath(contract => contract.Equipment.Name, opt => opt.MapFrom(src => src.EquipmentName))
            .ForMember(contract => contract.Quantity, opt => opt.MapFrom(src => src.EquipmentQuantity))
            .ReverseMap();
    }
}