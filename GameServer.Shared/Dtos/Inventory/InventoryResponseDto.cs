using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Shared.Dtos.Inventory
{
    public class InventoryResponseDto
    {
        public long ItemUID { get; set; }
        public int TemplateID { get; set; }
        public int Count { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
    public class CreateInventoryResponseDto
    {

    }

    public class DeleteInventoryResponseDto
    {

    }

    public class UpdateInventoryResponseDto
    {

    }
}
