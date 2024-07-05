using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using U4.Application.Restaurants.DTOs;
using U4.Domain.Exeptions;
using U4.Domain.Repositories;

namespace U4.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger,IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
    {
        public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting Restaurant by id {Id}", request.Id );
            var restaurant = await restaurantsRepository.GetAsync(request.Id);
            if (restaurant is null)
            {
                throw new NotFoundExeption("Restaurant not found");
            }

            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }
    }
}
