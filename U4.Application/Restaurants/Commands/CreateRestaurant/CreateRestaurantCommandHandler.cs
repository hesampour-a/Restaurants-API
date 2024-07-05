using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U4.Application.Restaurants.DTOs;
using U4.Application.User;
using U4.Domain.Constants;
using U4.Domain.Entities;
using U4.Domain.Exeptions;
using U4.Domain.Interfaces;
using U4.Domain.Repositories;

namespace U4.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(IRestaurantAuthorizationService restaurantAuthorizationService,IUserContext userContext,ILogger<CreateRestaurantCommandHandler> logger,IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("Creating Restaurant");
            var restaurant = mapper.Map<Restaurant>(request);

            restaurant.OwnerId = user.Id;

            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Create))
            {
                throw new ForbidExeption("You are not allowed to create this restaurant");
            }
            return restaurantsRepository.AddAsync(restaurant);
        }
    }
}
