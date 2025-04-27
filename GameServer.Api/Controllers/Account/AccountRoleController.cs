using GameServer.Application.Features.AccountRole.Commands;
using GameServer.Application.Features.AccountRole.Queries;
using GameServer.Shared.Dtos.AccountRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameServer.Api.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountRoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountRoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AccountRoleResponseDto>))]
        public async Task<IActionResult> GetAsync([FromQuery] GetAccountRoleQuery request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [Authorize]
        [ProducesResponseType(201, Type = typeof(CreateAccountRoleResponseDto))]
        public async Task<IActionResult> PostAsync([FromBody] CreateAccountRoleCommand request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(UpdateAccountRoleResponseDto))]
        public async Task<IActionResult> PutAsync([FromBody] UpdateAccountRoleCommand request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(DeleteAccountRoleResponseDto))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var res = await _mediator.Send(new DeleteAccountRoleCommand(id));
            return Ok(res);
        }
    }
}
