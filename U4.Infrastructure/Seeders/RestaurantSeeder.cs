using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using U4.Domain.Constants;
using U4.Domain.Entities;
using U4.Infrastructure.Persistence;

namespace U4.Infrastructure.Seeders
{
    public class RestaurantSeeder(IPasswordHasher<User> passwordHasher,RestaurantDbContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!(await dbContext.Restaurants.AnyAsync()))
                {
                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();
                }
                if (!(await dbContext.Roles.AnyAsync()))
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }
                if (!(await dbContext.Users.Where(u => u.Email == "admin4@test.com").AnyAsync()))
                {
                    var users = GetUsers();
                    dbContext.Users.AddRange(users);
                    await dbContext.SaveChangesAsync();

                    await AssignAdminRole();
                }
            }
        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles = new()
            {
                new (UserRoles.Admin)
                {
                    NormalizedName = UserRoles.Admin.ToUpper()
                },
                new(UserRoles.User)
                {
                    NormalizedName = UserRoles.User.ToUpper()
                },
                new(UserRoles.Owner)
                {
                    NormalizedName = UserRoles.Owner.ToUpper()
                },
            };
            return roles;
        }
        private IEnumerable<User> GetUsers()
        {
            List<User> users = new()
            {
                new User
                {
                    Email = "admin4@test.com",
                    NormalizedEmail = "ADMIN4@TEST.COM",
                    UserName = "admin4@test.com",
                    NormalizedUserName = "ADMIN4@TEST.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "P@ssword1"),

                }
            };
            return users;
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new()
            {
                new Restaurant
                {
                    Name = "McDonalds",
                    Description = "Fast food restaurant",
                    Category = "Fast food",
                    HasDelivery = true,
                    ContactEmail = "pEoLZ@example.com",
                    ContactNumber = "123-456-7890",
                    Address = new Address
                    {
                        Street = "123 Main Street",
                        City = "New York",
                        PostalCode = "10001"
                     },
                     Dishes = [
                        new ()
                        {
                            Name = "Big Mac",
                            Description = "Big Mac",
                            Price = 5.99M
                        },
                        new(){
                            Name = "Cheeseburger",
                            Description = "Cheeseburger",
                            Price = 4.99M
                        },
                     ]
                 },

                new Restaurant{
                    Name = "KFC",
                    Description = "Fast food restaurant",
                    Category = "Fast food",
                    HasDelivery = true,
                    ContactEmail = "pEoLZ@example.com",
                    ContactNumber = "123-456-7890",
                    Address = new Address
                    {
                        Street = "123 Main Street",
                        City = "New York",
                        PostalCode = "10001"
                     },
                     Dishes =
                     [
                        new ()
                        {
                            Name = "KFC Big Mac",
                            Description = "KFC Big Mac",
                            Price = 5.99M
                        },
                        new(){
                            Name = "KFC Cheeseburger",
                            Description = "KFC Cheeseburger",
                            Price = 4.99M
                    }]
                }
            };
            return restaurants;
        }

        public async Task AssignAdminRole()
        {
            var user = await userManager.FindByEmailAsync("admin4@test.com");
            // var role = await roleManager.FindByNameAsync(UserRoles.Admin);
            await userManager.AddToRoleAsync(user, UserRoles.Admin);
        }
    }
}