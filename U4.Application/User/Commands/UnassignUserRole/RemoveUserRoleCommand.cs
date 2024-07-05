using MediatR;

namespace U4.Application.User.Commands.UnassignUserRole;

public class RemoveUserRoleCommand :IRequest
{
    public string UserEmail { get; set; }
    public string RoleName { get; set; }
}
