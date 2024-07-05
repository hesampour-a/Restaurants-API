using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U4.Domain.Entities;

namespace U4.Application.Dishes.DTOs
{
    public class DishDto
    {
        public int Id { get; set; }
        public int? KiloCalories { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public static DishDto FromEntity(Dish dish)
        {
            return new DishDto
            {
                Id = dish.Id,
                KiloCalories = dish.KiloCalories,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price
            };
        }
       
    }
}
