using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using U4.Domain.Entities;

namespace U4.Infrastructure.Persistence;

public class RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : IdentityDbContext<User>(options)
{
    internal DbSet<Restaurant> Restaurants { get; set; }
    internal DbSet<Dish> Dishes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>()
          .OwnsOne(r => r.Address);

        modelBuilder.Entity<Restaurant>()
          .HasMany(r => r.Dishes)
          .WithOne()
          .HasForeignKey(d => d.ResaurantId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.OwnedRestaurants)
            .WithOne(r=>r.Owner)
            .HasForeignKey(r => r.OwnerId);
    }

}