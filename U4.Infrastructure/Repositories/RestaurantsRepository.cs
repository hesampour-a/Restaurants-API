using Microsoft.EntityFrameworkCore;
using U4.Domain.Entities;
using U4.Domain.Repositories;
using U4.Infrastructure.Persistence;

namespace U4.Infrastructure.Repositories
{
    internal class RestaurantsRepository(RestaurantDbContext dbContext) : IRestaurantsRepository
    {
        public async Task<int> AddAsync(Restaurant restaurant)
        {
            await dbContext.Restaurants.AddAsync(restaurant);
            await dbContext.SaveChangesAsync();
            return restaurant.Id;
        }

        public async Task<Restaurant?> DeleteAsync(int id)
        {
            var restaurant = dbContext.Restaurants.FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
            {
                return null;
            }

            dbContext.Restaurants.Remove(restaurant);
            await dbContext.SaveChangesAsync();

            return restaurant;
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await dbContext.Restaurants.Include(x => x.Dishes).ToListAsync();

            return restaurants;
        }

        public async Task<(IEnumerable<Restaurant>,int)> GetAllMachesAsync(string? searchPhrase,int PageSize,int PageNumber)
        {
            if (searchPhrase != null)
            {
                var lowerSearchPhrase = searchPhrase.ToLower();

                var query = dbContext.Restaurants.Include(x => x.Dishes).Where(r => r.Name.ToLower().Contains(lowerSearchPhrase) || r.Description.ToLower().Contains(lowerSearchPhrase));
                var totalCount = await query.CountAsync();
                var restaurants = await query.Skip(PageSize * (PageNumber - 1)).Take(PageSize).ToListAsync();
                return (restaurants, totalCount);
            }
            else
            {
                var query = dbContext.Restaurants.Include(x => x.Dishes);
                var totalCount = await query.CountAsync();
                var restaurants = await query.Skip(PageSize * (PageNumber - 1)).Take(PageSize).ToListAsync();
                return (restaurants, totalCount);
            }
        }

        public async Task<Restaurant?> GetAsync(int id)
        {
            var restaurant = await dbContext.Restaurants
                .Include(x => x.Dishes)
                .FirstOrDefaultAsync(x => x.Id == id);
            return restaurant;
        }

        public async Task<Restaurant?> UpdateAsync(Restaurant restaurant, int restaurantId)
        {
            var existingRestaurant = dbContext.Restaurants.FirstOrDefault(r => r.Id == restaurantId);
            if (existingRestaurant == null)
            {
                return null;
            }
            existingRestaurant.Name = restaurant.Name;
            existingRestaurant.Description = restaurant.Description;
            existingRestaurant.Category = restaurant.Category;
            existingRestaurant.HasDelivery = restaurant.HasDelivery;
            existingRestaurant.ContactEmail = restaurant.ContactEmail;
            existingRestaurant.ContactNumber = restaurant.ContactNumber;
            existingRestaurant.Address.Street = restaurant.Address.Street;
            existingRestaurant.Address.City = restaurant.Address.City;
            existingRestaurant.Address.PostalCode = restaurant.Address.PostalCode;
            await dbContext.SaveChangesAsync();

            return existingRestaurant;
        }

    }
}

