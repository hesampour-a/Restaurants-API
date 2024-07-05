using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using U4.Application.Restaurants.DTOs;
using U4.Domain.Constants;
using U4.Domain.Exeptions;
using U4.Domain.Interfaces;
using U4.Domain.Repositories;

namespace U4.Application.Restaurants.Commands.RemoveRestaurant
{
    public class RemoveRestaurantByIdCommandHandler(IRestaurantAuthorizationService restaurantAuthorizationService,ILogger<RemoveRestaurantByIdCommandHandler> logger
        ,IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<RemoveRestaurantByIdCommand, RestaurantDto>
    {
        public async Task<RestaurantDto> Handle(RemoveRestaurantByIdCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Removing Restaurant");
            var r = await restaurantsRepository.GetAsync(request.Id);
            if (!restaurantAuthorizationService.Authorize(r, ResourceOperation.Create))
            {
                throw new ForbidExeption("You are not allowed to create this restaurant");
            }
            var restaurant = await restaurantsRepository.DeleteAsync(request.Id);
            if (restaurant is null)
            {
                throw new NotFoundExeption("Restaurant not found");
            }
            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }
    }
}
