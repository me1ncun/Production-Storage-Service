using AutoMapper;
using Storage.Application.Models;
using Storage.Core.Enitities;

namespace Storage.Application.MappingProfiles;

public class ProductionFacilityProfile : Profile
{
    public ProductionFacilityProfile()
    {
        CreateMap<ProductionFacilityResponseModel, ProductionFacility>()
            .ForMember(f => f.Code, opt => opt.MapFrom(src => src.Id))
            .ForMember(f => f.StandartArea, opt => opt.MapFrom(src => src.StandartArea))
            .ReverseMap();
    }
}