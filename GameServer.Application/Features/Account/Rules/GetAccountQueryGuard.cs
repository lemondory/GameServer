using GameServer.Application.Features.Account.Queries;

namespace GameServer.Application.Features.Auth.Rules
{
    public static class GetAccountQueryGuard
    {
        public static GetAccountQueryGuardClause Against(GetAccountQuery request)
        {
            return new GetAccountQueryGuardClause(request);
        }
        public class GetAccountQueryGuardClause
        {
            private readonly GetAccountQuery _getAccountQuery;
            public GetAccountQueryGuardClause(GetAccountQuery getAccountQuery)
            {
                _getAccountQuery = getAccountQuery;
            }

            public GetAccountQueryGuardClause MustBePositive()
            {
                // Client Packet 검증 코드 추가 

                return this;
            }
        }
    }
}
