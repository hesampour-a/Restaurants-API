using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using U4.Application.Dishes.DTOs;
using U4.Domain.Exeptions;
using U4.Domain.Repositories;

namespace U4.Application.Dishes.Queries.GetAllDishesForRestaurant;

public class GetAllDishesForRestaurantQueryHandler(ILogger<GetAllDishesForRestaurantQueryHandler> logger,IMapper mapper, IDishesRepository dishesRepository,IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetAllDishesForRestaurantQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetAllDishesForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting All Dishes fo Restaurant by Id :" + request.Id);
        var restaurant =await restaurantsRepository.GetAsync(request.Id);
        if (restaurant == null) throw new NotFoundExeption(nameof(restaurant) + "by Id :" + request.Id.ToString() + " not found.");

        var dishes = restaurant.Dishes;

        if (dishes == null) throw new NotFoundExeption(nameof(restaurant) + "by Id :" + request.Id.ToString() + " has no dishes");

        var dishDtos = mapper.Map<IEnumerable<DishDto>>(dishes);
        return dishDtos;
    }
}
