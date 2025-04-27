using GameServer.Application.Features.Inventory.Command;
using GameServer.Application.Features.Inventory.Queries;
using GameServer.Shared.Dtos.Inventory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameServer.Api.Controllers.Inventory
{
    [Route("api/[controller]")]
    [ApiController]

    public class InventoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public InventoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(IEnumerable<InventoryResponseDto>))]
        public async Task<IActionResult> GetAsync([FromQuery] GetInventoryQuery request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        [ProducesResponseType(201, Type = typeof(CreateInventoryResponseDto))]
        public async Task<IActionResult> PostAsync([FromBody] CreateInventoryCommand request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPut]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(UpdateInventoryResponseDto))]
        public async Task<IActionResult> PutAsync([FromBody] UpdateInventoryCommand request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(CreateInventoryResponseDto))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var res = await _mediator.Send(new DeleteInventoryCommand(id));
            return Ok(res);
        }
    }
}
