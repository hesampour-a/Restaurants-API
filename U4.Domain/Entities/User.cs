using Microsoft.AspNetCore.Identity;

namespace U4.Domain.Entities;

public class User : IdentityUser
{
    public DateOnly? BirthDate { get; set; }
    public string? Nationality { get; set; }

    public List<Restaurant> OwnedRestaurants { get; set; } = [];
}
