using U4.Domain.Entities;

namespace U4.Domain.Repositories;

public interface IDishesRepository
{
    Task<int> AddAsync(Dish dish);
    Task<bool> RemoveAllDishes(IEnumerable<Dish> dishes);
}
