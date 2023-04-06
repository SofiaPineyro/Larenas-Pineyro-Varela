using System;
using System.Linq;

namespace ArenaGestor.BusinessHelpers
{
    public class StringGenerator
    {
        public static string GenerateRandomToken(int length)
        {
            var allChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrestuvwxyz0123456789";
            var random = new Random();
            var resultToken = new string(
               Enumerable.Repeat(allChar, length)
               .Select(token => token[random.Next(token.Length)]).ToArray());

            return resultToken.ToString();
        }
    }
}
