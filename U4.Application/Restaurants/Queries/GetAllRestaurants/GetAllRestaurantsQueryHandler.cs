using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using U4.Application.Common;
using U4.Application.Restaurants.DTOs;
using U4.Domain.Repositories;

namespace U4.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger,IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDto>>
    {
        public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {

            logger.LogInformation("Getting All Restaurants");
            var (restaurants, totalItemsCount) = await restaurantsRepository.GetAllMachesAsync(request.searchPhrase,request.PageSize,request.PageNumber);
            var restaurantDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

            var result = new PagedResult<RestaurantDto>(restaurantDtos,totalItemsCount,request.PageSize,request.PageNumber);
            return result!;
        }
    }
}

