using MediatR;

namespace U4.Application.Dishes.Commands.RemoveAllDishesForRestaurant;

public class RemoveAllDishesForRestaurantCommand(int restaurantId) : IRequest<bool>
{
    public int RestaurantId { get; } = restaurantId;
}
