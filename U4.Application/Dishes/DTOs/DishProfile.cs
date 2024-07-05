using AutoMapper;
using U4.Application.Dishes.Commands.CreateDishes;
using U4.Domain.Entities;

namespace U4.Application.Dishes.DTOs
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<CreateDishCommand, Dish>();
            CreateMap<Dish, DishDto>();
        }
    }
}
