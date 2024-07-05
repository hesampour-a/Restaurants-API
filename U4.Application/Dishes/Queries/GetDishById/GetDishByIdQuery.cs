using MediatR;
using U4.Application.Dishes.DTOs;

namespace U4.Application.Dishes.Queries.GetDishById;

public class GetDishByIdQuery(int restaurantId, int dishId) : IRequest<DishDto>
{
    public int RestaurantId { get; } = restaurantId;
    public int DishId { get; } = dishId;
}
