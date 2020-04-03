using AutoMapper;
using MFR.Core.DTO.Request;
using MFR.Core.DTO.Response;
using MFR.DomainModels;

namespace MFR.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MenuRequest, Menu>().ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name));

            CreateMap<Menu, MenuResponse>().ForMember(dest => dest.MenuId, map => map.MapFrom(src => src.MenuId))
                                           .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name));

            CreateMap<SubMenuRequest, SubMenu>().ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name))
                                                .ForMember(dest => dest.Description, map => map.MapFrom(src => src.Description))
                                                .ForMember(dest => dest.Price, map => map.MapFrom(src => src.Price));

            CreateMap<SubMenu, SubMenuResponse>().ForMember(dest => dest.SubMenuId, map => map.MapFrom(src => src.SubMenuId))
                                                 .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name))
                                                 .ForMember(dest => dest.Description, map => map.MapFrom(src => src.Description))
                                                 .ForMember(dest => dest.Price, map => map.MapFrom(src => src.Price));

            CreateMap<OrderRequest, Order>().ForMember(dest => dest.Reservation, map => map.MapFrom(src => new Reservation 
                                                                                               {Date = src.Date,
                                                                                               Time = src.Time,
                                                                                               NumberOfPeople = src.NumberOfPeople}));
        }
    }
}
