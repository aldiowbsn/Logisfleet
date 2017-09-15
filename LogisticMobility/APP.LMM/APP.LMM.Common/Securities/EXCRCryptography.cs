using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace APP.LMM.Common.Securities
{
    public class EXCRCryptography
    {
        #region Private Constant
        private const int SALT_BYTE_SIZE = 24;
        private const int HASH_BYTE_SIZE = 24;
        private const int PBKDF2_ITERATIONS = 1000;
        private const int ITERATION_INDEX = 0;
        private const int SALT_INDEX = 1;
        private const int PBKDF2_INDEX = 2;
        #endregion

        #region Public Method and Properties

        // Summary:
        //     Create One way Encryption RNGCryptoService
        public static string CreateHash(string password)
        {
            // Generate a random salt
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SALT_BYTE_SIZE];
            csprng.GetBytes(salt);

            // Hash the password and encode the parameters
            byte[] hash = PBKDF2(password.Trim(), salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
            return string.Format("{0}:{1}:{2}", PBKDF2_ITERATIONS, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        // Summary:
        //     Compare One way Encryption RNGCryptoService
        public static bool ValidatePassword(string password, string correctHash)
        {
            // Extract the parameters from the hash
            string[] split = correctHash.Split(':');
            int iterations = Int32.Parse(split[ITERATION_INDEX]);
            byte[] salt = Convert.FromBase64String(split[SALT_INDEX]);
            byte[] hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

            return SlowEquals(hash, PBKDF2(password, salt, iterations, hash.Length));
        }

        // Summary:
        //     Create SHA1Managed Encryption 
        public static string SHA1CreateHash(string input)
        {
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
        }

        #endregion

        #region Private Method and Properties
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
        #endregion
    }
}
