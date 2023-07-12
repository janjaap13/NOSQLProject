using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Konscious.Security.Cryptography;

namespace GardenGroupModel
{
    public class Security
    {
        public static byte[] GenerateSalt()
        {
            byte[] saltBytes = new byte[32];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetNonZeroBytes(saltBytes);
            }

            return saltBytes;
        }

        public static byte[] HashFunction(byte[] valueToHash, byte[] salt)
        {
            Argon2i argon2 = new Argon2i(valueToHash);
            argon2.DegreeOfParallelism = 16;
            argon2.MemorySize = 8192;
            argon2.Iterations = 40;
            argon2.Salt = salt;

            return argon2.GetBytes(512);
        }
    }
}
