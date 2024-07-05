using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using U4.Domain.Entities;
using U4.Domain.Repositories;

namespace U4.Application.Dishes.Commands.CreateDishes;

public class CreateDishCommandHandler(IMapper mapper,ILogger<CreateDishCommandHandler> logger,IDishesRepository dishesRepository,IRestaurantsRepository restaurantsRepository) : IRequestHandler<CreateDishCommand>
{
    public async Task Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating Dish");
        var restaurant =await restaurantsRepository.GetAsync(request.ResaurantId);
        if(restaurant == null) throw new DirectoryNotFoundException(nameof(restaurant) + "by Id :" + request.ResaurantId.ToString() + " not found");

        var dish = mapper.Map<Dish>(request);
        await dishesRepository.AddAsync(dish);
    }
}
