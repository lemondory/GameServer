using GameServer.Application.Features.Auth.Command;
using GameServer.Infrastructure.Errors;

namespace GameServer.Application.Features.Auth.Rules
{
    public static class GetAuthQueryGuard
    {
        public static GetAuthQueryGuardClause Against(SocialLoginCommand request)
        {
            return new GetAuthQueryGuardClause(request);
        }
        public class GetAuthQueryGuardClause
        {
            private readonly SocialLoginCommand _socialLoginCommand;
            public GetAuthQueryGuardClause(SocialLoginCommand socialLoginCommand)
            {
                _socialLoginCommand = socialLoginCommand;
            }

            public GetAuthQueryGuardClause MustBePositive()
            {
                if (_socialLoginCommand.Request.Provider < 0)
                    throw new BusinessException("ExternalProvider id is incorrect");

                if (string.IsNullOrEmpty(_socialLoginCommand.Request.AuthToken))
                    throw new BusinessException("ExternalId id is incorrect");

                return this;
            }
        }
    }
}
