using GameServer.Application.Features.Account.Commands;
using GameServer.Contracts.Infrastructure;
using GameServer.Contracts.Repositories;
using GameServer.Domain.Enums;
using GameServer.Infrastructure.Result;
using GameServer.Shared.Dtos.Account;
using GameServer.Shared.Enums;
using MediatR;

namespace GameServer.Application.Features.Account.Handlers
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, IRequestDataResult<CreateAccountResponseDto>> 
    {
        private readonly IAccountRepository _accountRepository;

        public CreateAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IRequestDataResult<CreateAccountResponseDto>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            // 명령 처리 로직 추가 

            return new RequestDataResult<CreateAccountResponseDto>(new CreateAccountResponseDto
            {
       
            }, true, ErrorCode.NONE);
        }
    }
}
