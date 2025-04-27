using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Domain.Entities
{
    public class CharacterEntity
    {
        public long CharacterUID { get; set; }
        public long AccountUID { get; set; }

        public string? CharacterName { get; set; }
        public int CharacterLevel { get; set; }
        public short CharacterClass { get; set; }
        public long Exp { get; set; } = 0;

        public int? ServerId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}