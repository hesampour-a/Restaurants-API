using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using U4.Domain.Entities;

namespace U4.Infrastructure.Authorization;

public class RestaurantUserClaimsPrincipalFactory(IOptions<IdentityOptions> options,UserManager<User> userManager,RoleManager<IdentityRole> roleManager) : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, options)
{
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var id = await GenerateClaimsAsync(user);

        if (user.Nationality != null) { 
        id.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality));
        }

        if (user.BirthDate != null) { 
        id.AddClaim(new Claim(AppClaimTypes.BirthDate, user.BirthDate.Value.ToString("yyyy-MM-dd")));
        }
        return new ClaimsPrincipal(id);
    }
}
