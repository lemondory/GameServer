using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Application.Common.Utilities
{
    public static class RandomStringGenerator
    {
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly Random _random = new();

        public static string Generate(int length = 12)
        {
            return new string(Enumerable.Range(0, length)
                .Select(_ => Chars[_random.Next(Chars.Length)])
                .ToArray());
        }
    }
}
