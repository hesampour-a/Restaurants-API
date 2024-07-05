using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using U4.Domain.Exeptions;

namespace U4.Application.User.Commands.AssignUserRole;

internal class AssignUserRoleCommandHandler(RoleManager<IdentityRole> roleManager, UserManager<Domain.Entities.User> userManager, ILogger<AssignUserRoleCommandHandler> logger) : IRequestHandler<AssignUserRoleCommand>
{
    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.UserEmail)
            ??
            throw new NotFoundExeption("user not found");

        var role = await roleManager.FindByNameAsync(request.RoleName)
            ??
            throw new NotFoundExeption("role not found");

        await userManager.AddToRoleAsync(user, role.Name);


    }
}
