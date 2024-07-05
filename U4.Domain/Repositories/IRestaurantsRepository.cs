using U4.Domain.Entities;

namespace U4.Domain.Repositories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<(IEnumerable<Restaurant>,int)> GetAllMachesAsync(string? searchPhrase,int PageSize,int PageNumber);
    Task<Restaurant?> GetAsync(int id);
    Task<int> AddAsync(Restaurant restaurant);
    Task<Restaurant?> DeleteAsync(int id);
    Task<Restaurant?> UpdateAsync(Restaurant restaurant , int restaurantId);
}
