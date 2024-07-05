using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using U4.Application.Dishes.DTOs;
using U4.Domain.Exeptions;
using U4.Domain.Repositories;

namespace U4.Application.Dishes.Queries.GetDishById;

public class GetDishByIdQueryHandler(ILogger<GetDishByIdQueryHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetDishByIdQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting Dish By Id :" + request.DishId.ToString() + " from restaurant by id :" + request.RestaurantId.ToString());


        var restaurant = await restaurantsRepository.GetAsync(request.RestaurantId);
        if (restaurant == null) throw new NotFoundExeption(nameof(restaurant) + "by Id :" + request.RestaurantId.ToString() + " not found.");

        var dish = restaurant.Dishes.FirstOrDefault(x => x.Id == request.DishId);
        if (dish == null) throw new NotFoundExeption(nameof(dish) + "by Id :" + request.DishId.ToString() + " not found.");

        var dishDto = mapper.Map<DishDto>(dish);
        return dishDto;
    }
}
