using AutoMapper;
using Storage.Application.Models;
using Storage.Core.Enitities;

namespace Storage.Application.MappingProfiles;

public class EquipmentProfile : Profile
{
    public EquipmentProfile()
    {
        CreateMap<EquipmentResponseModel, Equipment>()
            .ForMember(e => e.Code, opt => opt.MapFrom(src => src.Id))
            .ForMember(e => e.Area, opt => opt.MapFrom(src => src.Area))
            .ReverseMap();
    }
}