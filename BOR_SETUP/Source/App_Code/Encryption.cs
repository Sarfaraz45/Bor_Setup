using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.IO;

/// <summary>
/// Summary description for Encryption
/// </summary>
public class Encryption
{
	

        // This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private static readonly byte[] initVectorBytes = Encoding.ASCII.GetBytes("tu89geji340t89u2");

        // This constant is used to determine the keysize of the encryption algorithm.
        private const int keysize = 256;

        static readonly string PasswordHash = "p@@sW0RDAQM";
        static readonly string SaltKey = "@QM&EMD_sOL";
        static readonly string VIKey = "@emDc3D4e5F6gAQM";

        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
            //byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            //using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
            //{
            //    byte[] keyBytes = password.GetBytes(keysize / 8);
            //    using (RijndaelManaged symmetricKey = new RijndaelManaged())
            //    {
            //        symmetricKey.Mode = CipherMode.CBC;
            //        using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes))
            //        {
            //            using (MemoryStream memoryStream = new MemoryStream())
            //            {
            //                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            //                {
            //                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            //                    cryptoStream.FlushFinalBlock();
            //                    byte[] cipherTextBytes = memoryStream.ToArray();
            //                    return Convert.ToBase64String(cipherTextBytes);
            //                }
            //            }
            //        }
            //    }
            //}
        }

        public static string Decrypt(string cipherText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());

            //byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            //using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
            //{
            //    byte[] keyBytes = password.GetBytes(keysize / 8);
            //    using (RijndaelManaged symmetricKey = new RijndaelManaged())
            //    {
            //        symmetricKey.Mode = CipherMode.CBC;
            //        using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes))
            //        {
            //            using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
            //            {
            //                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
            //                {
            //                    byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            //                    int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            //                    return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            //                }
            //            }
            //        }
            //    }
            //}
        }
	
}