using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Shared.Dtos.Character
{
    public class CharacterSummaryDto
    {
        public long CharacterUID { get; set; }
        public string? CharacterName { get; set; }
        public int CharacterLevel { get; set; }
        public short CharacterClass { get; set; }
        public long Exp { get; set; }
        public int? ServerId { get; set; }
    }
}
