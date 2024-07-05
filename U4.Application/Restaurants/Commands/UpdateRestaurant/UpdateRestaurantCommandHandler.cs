using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U4.Application.Restaurants.DTOs;
using U4.Domain.Constants;
using U4.Domain.Entities;
using U4.Domain.Exeptions;
using U4.Domain.Interfaces;
using U4.Domain.Repositories;

namespace U4.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(IRestaurantAuthorizationService restaurantAuthorizationService,ILogger<UpdateRestaurantCommandHandler> logger,IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<UpdateRestaurantCommand, RestaurantDto>
    {
        public async Task<RestaurantDto> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating Restaurant");
            var r = await restaurantsRepository.GetAsync(request.Id);
            if (!restaurantAuthorizationService.Authorize(r, ResourceOperation.Create))
            {
                throw new ForbidExeption("You are not allowed to create this restaurant");
            }
            var restaurant = await restaurantsRepository.UpdateAsync(mapper.Map<Restaurant>(request),request.Id);

            if (restaurant is null)
            {
                throw new NotFoundExeption("Restaurant not found");
            }

            return mapper.Map<RestaurantDto>(restaurant);
            

        }
    }
}
