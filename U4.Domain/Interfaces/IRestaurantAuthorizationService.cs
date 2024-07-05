using U4.Domain.Constants;
using U4.Domain.Entities;


namespace U4.Domain.Interfaces
{
    public interface IRestaurantAuthorizationService
    {
        bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation);
    }
}