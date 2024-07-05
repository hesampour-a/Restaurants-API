using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using U4.Domain.Exeptions;

namespace U4.Application.User.Commands;

public class UpdateUserCommandHandler(IUserStore<Domain.Entities.User> userStore, IUserContext userContext, ILogger<UpdateUserCommandHandler> logger) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating User");
        
        var user = userContext.GetCurrentUser();

        var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);
        if (dbUser is null)
        {
          throw new NotFoundExeption("User not found");
        }
        dbUser.Nationality = request.Nationality;
        dbUser.BirthDate = request.BirthDate;

        await userStore.UpdateAsync(dbUser, cancellationToken);
    
    }
}
