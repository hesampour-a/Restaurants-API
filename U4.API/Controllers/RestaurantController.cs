using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using U4.Application.Restaurants.Commands.CreateRestaurant;
using U4.Application.Restaurants.Commands.RemoveRestaurant;
using U4.Application.Restaurants.Commands.UpdateRestaurant;
using U4.Application.Restaurants.DTOs;
using U4.Application.Restaurants.Queries.GetAllRestaurants;
using U4.Application.Restaurants.Queries.GetRestaurantById;
using U4.Infrastructure.Authorization;

namespace U4.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RestaurantController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll([FromQuery] GetAllRestaurantsQuery query)
        {
            var restaurants = await mediator.Send(query);
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PoloicyNames.HasNationality)]
        public async Task<ActionResult<RestaurantDto?>> GetById([FromRoute] int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));


            return Ok(restaurant);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateRestaurantCommand command)
        {
            var restaurantId = await mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = restaurantId }, null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var restaurant = await mediator.Send(new RemoveRestaurantByIdCommand(id));

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRestaurantCommand command)
        {
            command.Id = id;
            var restaurant = await mediator.Send(command);

            return Ok(restaurant);
        }
    }
}
