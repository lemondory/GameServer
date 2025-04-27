using GameServer.Application.Features.Inventory.Handlers;
using GameServer.Application.Features.Inventory.Queries;
using GameServer.Infrastructure.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Application.Features.Inventory.Rules
{
    public static class GetInventoryQueryGuard
    {
        public static GetInventoryQueryClause Against(GetInventoryQuery request)
        {
            return new GetInventoryQueryClause(request);
        }
        public class GetInventoryQueryClause
        {
            private readonly GetInventoryQuery _inventory;
            public GetInventoryQueryClause(GetInventoryQuery inventory)
            {
                _inventory = inventory;
            }

            public GetInventoryQueryClause MustBePositive()
            {
                if (_inventory.InventoryUID < 1)
                    throw new BusinessException("Inventory id is incorrect");

                return this;
            }
        }
    }
}
