using U4.Application.Dishes.DTOs;
using U4.Domain.Entities;

namespace U4.Application.Restaurants.DTOs
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public bool HasDelivery { get; set; }

        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }

        public List<DishDto> Dishes { get; set; } = [];

        public static RestaurantDto? FromEntity(Restaurant? restaurant)
        {
            if (restaurant == null) return null;
            return new RestaurantDto
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                Category = restaurant.Category,
                HasDelivery = restaurant.HasDelivery,
                City = restaurant.Address.City,
                Street = restaurant.Address.Street,
                PostalCode = restaurant.Address.PostalCode,
                Dishes = restaurant.Dishes.Select(DishDto.FromEntity).ToList()
            };
        }
    }

}
