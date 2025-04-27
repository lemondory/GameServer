using GameServer.Application.Features.Account.Commands;
using GameServer.Application.Features.Account.Queries;
using GameServer.Shared.Dtos.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameServer.Api.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AccountResponseDto>))]
        public async Task<IActionResult> GetAsync([FromQuery] GetAccountQuery request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [Authorize] 
        [ProducesResponseType(201, Type = typeof(CreateAccountResponseDto))]
        public async Task<IActionResult> PostAsync([FromBody] CreateAccountCommand request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(UpdateAccountResponseDto))]
        public async Task<IActionResult> PutAsync([FromBody] UpdateAccountCommand request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(DeleteAccountResponseDto))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var res = await _mediator.Send(new DeleteAccountCommand(id));
            return Ok(res);
        }
    }
}
