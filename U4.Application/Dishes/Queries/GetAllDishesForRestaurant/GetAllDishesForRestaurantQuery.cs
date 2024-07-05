using MediatR;
using U4.Application.Dishes.DTOs;

namespace U4.Application.Dishes.Queries.GetAllDishesForRestaurant;

public class GetAllDishesForRestaurantQuery(int id) : IRequest<IEnumerable<DishDto>>
{
    public int Id { get; set; } = id;
}
