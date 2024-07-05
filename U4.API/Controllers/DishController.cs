using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using U4.Application.Dishes.Commands.CreateDishes;
using U4.Application.Dishes.Commands.RemoveAllDishesForRestaurant;
using U4.Application.Dishes.DTOs;
using U4.Application.Dishes.Queries.GetAllDishesForRestaurant;
using U4.Application.Dishes.Queries.GetDishById;

namespace U4.API.Controllers
{
    [Route("api/restaurants/{restaurantId}/[controller]")]
    [ApiController]
    public class DishController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restaurantId)
        {
            var dishes = await mediator.Send(new GetAllDishesForRestaurantQuery(restaurantId));
            return Ok(dishes);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DishDto>> GetDishById([FromRoute] int restaurantId, [FromRoute] int id)
        {
            var dish = await mediator.Send(new GetDishByIdQuery(restaurantId, id));
            return Ok(dish);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromRoute] int restaurantId, [FromBody] CreateDishCommand command)
        {
            command.ResaurantId = restaurantId;
            await mediator.Send(command);
            return Created();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllForRestaurant([FromRoute] int restaurantId)
        {
            await mediator.Send(new RemoveAllDishesForRestaurantCommand(restaurantId));
            return NoContent();
        }
        
    }
}
