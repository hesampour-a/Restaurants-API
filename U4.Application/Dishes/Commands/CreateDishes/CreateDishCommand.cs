using MediatR;

namespace U4.Application.Dishes.Commands.CreateDishes;

public class CreateDishCommand : IRequest
{
    public int? KiloCalories { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int ResaurantId { get; set; }
}
