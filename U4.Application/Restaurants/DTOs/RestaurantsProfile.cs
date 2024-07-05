using AutoMapper;
using U4.Application.Restaurants.Commands.CreateRestaurant;
using U4.Application.Restaurants.Commands.UpdateRestaurant;
using U4.Domain.Entities;

namespace U4.Application.Restaurants.DTOs
{
    public class RestaurantsProfile : Profile
    {
        public RestaurantsProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(x => x.City, opt =>
                opt.MapFrom(src => src.Address == null ? null : src.Address.City))
                .ForMember(x => x.Street, opt =>
                opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
                .ForMember(x => x.PostalCode, opt =>
                opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
                .ForMember(x => x.Dishes, opt => opt.MapFrom(src => src.Dishes));

            CreateMap<CreateRestaurantCommand, Restaurant>()
                .ForMember(d=>d.Address , opt => opt.MapFrom(src => new Address
                {
                    City = src.City,
                    Street = src.Street,
                    PostalCode = src.PostalCode
                }));

            CreateMap<UpdateRestaurantCommand, Restaurant>()
               .ForMember(d => d.Address, opt => opt.MapFrom(src => new Address
               {
                   City = src.City,
                   Street = src.Street,
                   PostalCode = src.PostalCode
               }));
        }
    }
}
