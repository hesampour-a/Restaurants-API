using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U4.Application.Restaurants.DTOs;

namespace U4.Application.Restaurants.Commands.RemoveRestaurant
{
    public class RemoveRestaurantByIdCommand(int id) : IRequest<RestaurantDto>
    {
        public int Id { get; } = id;
    }
}
