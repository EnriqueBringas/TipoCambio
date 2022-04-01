using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;

namespace TipoCambio.Api.Core.Utility.Helpers
{
    public class Hash
    {
        public static void SetHash(string value, out string hash, out string salt)
        {
            var lsalt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(lsalt);
            }

            var lhashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: value,
                salt: lsalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            hash = lhashed;
            salt = Convert.ToBase64String(lsalt);
        }

        public static bool CheckHash(string value, string hash, string salt)
        {
            var lhashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                 password: value,
                 salt: Convert.FromBase64String(salt),
                 prf: KeyDerivationPrf.HMACSHA256,
                 iterationCount: 10000,
                 numBytesRequested: 256 / 8));

            return hash == lhashed;
        }

        public static byte[] GetHash(string value, string salt)
        {
            var lunhashed = Encoding.Unicode.GetBytes(string.Concat(salt, value));
            var lsha256 = new SHA256Managed();
            var lhashed = lsha256.ComputeHash(lunhashed);

            return lhashed;
        }
    }
}