using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using U4.Application.User.Commands;
using U4.Application.User.Commands.AssignUserRole;
using U4.Application.User.Commands.UnassignUserRole;
using U4.Domain.Constants;

namespace U4.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPatch("user")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPost("userRole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> AssignUserRole([FromBody] AssignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
        [HttpDelete("userRole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> RemoveUserRole([FromBody] RemoveUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
    }
}
