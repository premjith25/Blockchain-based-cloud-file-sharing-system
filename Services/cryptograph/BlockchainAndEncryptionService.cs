using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace CloudFileShare.Services
{
    public class BlockchainAndEncryptionService
    {
        private static readonly string EncryptionKey = "B14nc3Cl0udSh4r3"; // 16-byte key for AES

        // 1. SHA-256 File Hashing
        public string GenerateFileHash(byte[] fileBytes)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(fileBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        // 2. AES Encryption for File Data
        public byte[] Encrypt(byte[] plainTextBytes)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                aes.IV = new byte[16]; // Default zero IV for simplicity in student project demo

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cs.FlushFinalBlock();
                    }
                    return ms.ToArray();
                }
            }
        }

        // 3. AES Decryption for File Download
        public byte[] Decrypt(byte[] cipherBytes)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                aes.IV = new byte[16];

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.FlushFinalBlock();
                    }
                    return ms.ToArray();
                }
            }
        }

        // 4. Add Block Method (Exposed to Person 4)
        public string AddBlock(string fileName, byte[] fileData, string previousHash)
        {
            string currentFileHash = GenerateFileHash(fileData);
            string blockData = fileName + currentFileHash + previousHash + DateTime.UtcNow.Ticks;
            
            // Generate current block hash (simulating blockchain chaining)
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(blockData));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        // 5. Verify File Tamper Status (Exposed to Person 4)
        public bool VerifyFile(string storedHash, byte[] currentFileBytes)
        {
            string newlyComputedHash = GenerateFileHash(currentFileBytes);
            return string.Equals(storedHash, newlyComputedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}