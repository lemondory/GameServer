using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Shared.Dtos.Character
{
    public class CreateCharacterRequestDto
    {
        public long AccountUID { get; set; }
        public string CharacterName { get; set; }
        public int ServerId { get; set; }
    }
}