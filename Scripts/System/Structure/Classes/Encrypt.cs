using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace UserSystemFramework.Scripts.System.Structure.Classes
{
    /// <summary>
    /// Simple encryption class using SHA256 hashing method with secret KEY/IV. Counterpart is on the server written in PHP under USF.Encrypt.php
    /// The key and iv can be changed to your liking but don't forget to do the same on the PHP counterpart or else it won't work.
    /// TODO: Better key/iv management to allow user definition.
    /// </summary>
    public static class Encrypt
    {
        private static readonly SHA256 SHA256Encryptor = SHA256Managed.Create();
        private static readonly byte[] Key = SHA256Encryptor.ComputeHash(Encoding.ASCII.GetBytes("3sc3RLrpd17"));
        private static readonly byte[] Iv = new byte[16] { 0x0, 0x0, 0x43, 0x24, 0x15, 0x14, 0x0, 0x0, 0x48, 0x0, 0x0, 0x0, 0x19, 0x0, 0x17, 0x0 };
        public static string EncryptAes(string plainText)
        {
            Aes encryptor = Aes.Create();
            encryptor.Mode = CipherMode.CBC;
            byte[] aesKey = new byte[32];
            Array.Copy(Key, 0, aesKey, 0, 32);
            encryptor.Key = aesKey;
            encryptor.IV = Iv;
            MemoryStream memoryStream = new MemoryStream();
            ICryptoTransform aesEncryptor = encryptor.CreateEncryptor();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write);
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);
            return cipherText;
        }

        public static string DecryptAes(string cipherText)
        {
            Aes encryptor = Aes.Create();
            encryptor.Mode = CipherMode.CBC;
            byte[] aesKey = new byte[32];
            Array.Copy(Key, 0, aesKey, 0, 32);
            encryptor.Key = aesKey;
            encryptor.IV = Iv;
            MemoryStream memoryStream = new MemoryStream();
            ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);
            string plainText;
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] plainBytes = memoryStream.ToArray();
                plainText = Encoding.UTF8.GetString(plainBytes, 0, plainBytes.Length);
            }
            finally
            {
                memoryStream.Close();
                cryptoStream.Close();
            }
            return plainText;
        }
    }
    /// <summary>
    /// Extension methods to return an encrypted/decrypted version of a string.
    /// </summary>
    public static class EncryptExtensions
    {
        /// <summary>
        /// Encrypts a string using SHA256 method.
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>Encrypted string</returns>
        public static string ToEncrypted(this string input)
        {
            return Encrypt.EncryptAes(input);
        }
        /// <summary>
        /// Decrypts a string using SHA256 method. To handle exceptions silently, this returns the original string in case the decryption fails.
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>Decrypted string if possible, original string otherwise.</returns>
        public static string ToDecrypted(this string input)
        {
            try
            {
                return Encrypt.DecryptAes(input);
            }
#pragma warning disable CS0168
            catch (Exception e)
#pragma warning restore CS0168
            {
                return input;
            }
        }
    }
}