using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using U4.Domain.Exeptions;

namespace U4.Application.User.Commands.UnassignUserRole;

public class RemoveUserRoleCommandHandler(RoleManager<IdentityRole> roleManager,UserManager<Domain.Entities.User> userManager,ILogger<RemoveUserRoleCommandHandler> logger) : IRequestHandler<RemoveUserRoleCommand>
{
    public async Task Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Removing Role from User");
        var user = await userManager.FindByEmailAsync(request.UserEmail) ?? throw new NotFoundExeption("User not found");

        var role = await roleManager.FindByNameAsync(request.RoleName) ?? throw new NotFoundExeption("Role not found");

        await userManager.RemoveFromRoleAsync(user, role.Name);
    }
}
