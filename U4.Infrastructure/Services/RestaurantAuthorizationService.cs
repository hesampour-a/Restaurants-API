using U4.Application.User;
using U4.Domain.Constants;
using U4.Domain.Entities;
using U4.Domain.Interfaces;


namespace U4.Infrastructure.Services;

public class RestaurantAuthorizationService(IUserContext userContext) : IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
    {
        var user = userContext.GetCurrentUser();

        if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
        {
            return true;
        }

        if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            return true;
        }

        if ((resourceOperation == ResourceOperation.Update || resourceOperation == ResourceOperation.Delete) && user.Id == restaurant.OwnerId)
        {
            return true;
        }

        return false;
    }
}
