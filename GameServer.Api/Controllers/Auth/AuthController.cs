using GameServer.Application.Features.Auth.Command;
using GameServer.Contracts.Infrastructure;
using GameServer.Shared.Dtos.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameServer.Api.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(IRequestDataResult<AuthResponseDto>))]
        public async Task<IActionResult> LoginAsync([FromBody] SocialLoginCommand request)
        {
            var result = await _mediator.Send(request);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(IRequestDataResult<RefreshTokenResponseDto>))]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenCommand request)
        {
            var result = await _mediator.Send(request);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
