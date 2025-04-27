using GameServer.Contracts.Services;
using Newtonsoft.Json;
using System.Text;

namespace GameServer.Persistance.Services
{
    public class JsonSerializer : ISerializer
    {
        public byte[] Serialize<T>(T value)
        {
            var json = JsonConvert.SerializeObject(value);
            return Encoding.UTF8.GetBytes(json);
        }

        public T Deserialize<T>(byte[] data)
        {
            var json = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject<T>(json)!;
        }
    }

     public class MessagePackSerializer : ISerializer
    {
        public byte[] Serialize<T>(T value)
        {
            return MessagePack.MessagePackSerializer.Serialize(value);
        }

        public T Deserialize<T>(byte[] data)
        {
            return MessagePack.MessagePackSerializer.Deserialize<T>(data);
        }
    }
}