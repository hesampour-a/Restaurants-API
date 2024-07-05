using MediatR;
using Microsoft.Extensions.Logging;
using U4.Domain.Exeptions;
using U4.Domain.Repositories;

namespace U4.Application.Dishes.Commands.RemoveAllDishesForRestaurant
{
    internal class RemoveAllDishesForRestaurantCommandHandler(IDishesRepository dishesRepository,ILogger<RemoveAllDishesForRestaurantCommandHandler> logger, IRestaurantsRepository restaurantsRepository) : IRequestHandler<RemoveAllDishesForRestaurantCommand, bool>
    {
        public async Task<bool> Handle(RemoveAllDishesForRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await restaurantsRepository.GetAsync(request.RestaurantId);
            if (restaurant == null) throw new NotFoundExeption(nameof(restaurant) + "by Id :" + request.RestaurantId.ToString() + " dose not exist.");

            var dishes = restaurant.Dishes;
            if (dishes == null) throw new NotFoundExeption(nameof(dishes) + "by Id :" + request.RestaurantId.ToString() + " dose not exist.");

            var isDeleted = await dishesRepository.RemoveAllDishes(dishes);
            return isDeleted;
        }
    }
}
