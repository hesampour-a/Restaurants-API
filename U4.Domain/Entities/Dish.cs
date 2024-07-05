using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace U4.Domain.Entities
{
    public class Dish
    {
        public int Id { get; set; }
        public int? KiloCalories { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int ResaurantId { get; set; }
    }
}