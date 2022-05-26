using System;
using System.Security.Cryptography;

namespace WPFUtilsLib.Helpers
{
    public class PasswordHasher
    {
        public static string HashPassword(string Password)
        {
            byte[] Salt = new byte[16];
            byte[] Hash = new byte[272];

            RNGCryptoServiceProvider Salter = new RNGCryptoServiceProvider();

            Salter.GetBytes(Salt);

            Rfc2898DeriveBytes Hasher = new Rfc2898DeriveBytes(Password, Salt, 10000);

            Array.Copy(Salt, 0, Hash, 0, 16);
            Array.Copy(Hasher.GetBytes(256), 0, Hash, 16, 256);

            return Convert.ToBase64String(Hash);
        }
    }    
}
