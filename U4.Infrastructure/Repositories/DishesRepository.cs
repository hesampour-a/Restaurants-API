using U4.Domain.Entities;
using U4.Domain.Repositories;
using U4.Infrastructure.Persistence;

namespace U4.Infrastructure.Repositories;

public class DishesRepository(RestaurantDbContext dbContext) : IDishesRepository
{
    public async Task<int> AddAsync(Dish dish)
    {
        await dbContext.AddAsync(dish);
        await dbContext.SaveChangesAsync();
        return dish.Id;
    }

    public async Task<bool> RemoveAllDishes(IEnumerable<Dish> dishes)
    {
        dbContext.RemoveRange(dishes);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
