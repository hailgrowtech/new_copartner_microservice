using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CommonLibrary;

public class EncryptionHelper
{
    private const string EncryptionKey = "key@2024$Encr%p*"; // Replace with your encryption key

    public static string EncryptString(string plainText)
    {
        byte[] encryptedBytes;
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            aesAlg.IV = new byte[16]; // IV is typically 16 bytes for AES

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encryptedBytes = msEncrypt.ToArray();
                }
            }
        }
        return Convert.ToBase64String(encryptedBytes);
    }

    public static string DecryptString(string encryptedText)
    {
        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
        string decryptedText;
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            aesAlg.IV = new byte[16]; // IV is typically 16 bytes for AES

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        decryptedText = srDecrypt.ReadToEnd();
                    }
                }
            }
        }
        return decryptedText;
    }
}
