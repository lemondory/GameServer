using GameServer.Application.Features.AccountRole.Queries;

namespace GameServer.Application.Features.Auth.Rules
{
    public static class GetAccountRoleQueryGuard
    {
        public static GetAccountRoleQueryGuardClause Against(GetAccountRoleQuery request)
        {
            return new GetAccountRoleQueryGuardClause(request);
        }
        public class GetAccountRoleQueryGuardClause
        {
            private readonly GetAccountRoleQuery _accountRoleQuery;
            public GetAccountRoleQueryGuardClause(GetAccountRoleQuery accountRoleQuery)
            {
                _accountRoleQuery = accountRoleQuery;
            }

            public GetAccountRoleQueryGuardClause MustBePositive()
            {
                // Client Packet 검증 코드 추가 

                return this;
            }
        }
    }
}
