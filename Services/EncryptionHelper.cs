using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    // Lớp hỗ trợ mã hóa và giải mã chuỗi
    public class EncryptionHelper
    {
        // Khóa bảo mật (key) và vector khởi tạo (IV)
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("A123456789012345"); // Key dài 16 byte
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("B123456789012345");  // IV dài 16 byte

        // Mã hóa chuỗi
        public static string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            // Ghi dữ liệu mã hóa vào stream
                            swEncrypt.Write(plainText);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        // Giải mã chuỗi
        public static string Decrypt(string cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Đọc chuỗi đã được giải mã từ stream
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
