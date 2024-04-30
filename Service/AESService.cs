using FastFood.Service.Interface; // Importing interface for AES service
using System.Security.Cryptography; // Importing namespace for cryptographic operations
using System.Text; // Importing namespace for string encoding

namespace FastFood.Service
{
    public class AESService : IAESService
    {
        private readonly ILogger<AESService> _logger; // Logger instance for logging
        private readonly string _key; // AES encryption key

        // Constructor for Configuration and AESService
        public AESService(IConfiguration configuration, ILogger<AESService> logger)
        {
            _logger = logger; // Initializing logger
            _key = configuration["AES:Key"]; // Retrieving AES key from configuration
            if (_key == null) { _key = "@*FastFoodKey24#"; } // Using default key if not provided in configuration
        }

        // Method to encrypt text asynchronously
        public async Task<string> Encrypt(string text)
        {
            byte[] encryptedBytes; // Byte array to store encrypted data
            byte[] iv; // Byte array to store initialization vector

            // Using AES for encryption
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GenerateKey(_key); // Generating key for AES encryption
                iv = aesAlg.IV; // Storing initialization vector

                // Creating encryptor using AES key and initialization vector
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Using memory stream to store encrypted data
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // Writing initialization vector to the beginning of the stream
                    msEncrypt.Write(iv, 0, iv.Length);

                    // Using CryptoStream for encryption
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            await swEncrypt.WriteAsync(text); // Writing text to be encrypted
                        }
                    }
                    encryptedBytes = msEncrypt.ToArray(); // Converting encrypted data to byte array
                }
            }

            // Converting IV + ciphertext to base64 string
            return Convert.ToBase64String(encryptedBytes);
        }

        // Method to decrypt ciphertext asynchronously
        public async Task<string> Decrypt(string cipherText)
        {
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText); // Converting base64 string to byte array
                byte[] iv = new byte[16]; // Initializing byte array for initialization vector
                Array.Copy(cipherBytes, 0, iv, 0, iv.Length); // Extracting initialization vector from cipherBytes

                // Using AES for decryption
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = GenerateKey(_key); // Generating key for AES decryption
                    aesAlg.IV = iv; // Setting initialization vector

                    // Creating decryptor using AES key and initialization vector
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    // Using memory stream to store decrypted data
                    using (MemoryStream msDecrypt = new MemoryStream(cipherBytes, iv.Length, cipherBytes.Length - iv.Length))
                    {
                        // Using CryptoStream for decryption
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return await srDecrypt.ReadToEndAsync(); // Reading decrypted text asynchronously
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while decrypting: {cipherText}"); // Logging decryption error
            }

            return ""; // Returning empty string if decryption fails
        }

        // Method to generate AES key from provided key
        private byte[] GenerateKey(string key)
        {
            using (var sha = SHA256.Create())
            {
                return sha.ComputeHash(Encoding.UTF8.GetBytes(key)); // Generating SHA-256 hash of the key
            }
        }

        // Method to generate random initialization vector
        private byte[] GenerateIV()
        {
            byte[] iv = new byte[16]; // Initializing byte array for initialization vector
            RandomNumberGenerator.Fill(iv); // Filling byte array with random values
            return iv; // Returning generated initialization vector
        }

        // Method to extract initialization vector from encrypted data
        private byte[] ExtractIV(byte[] encryptedData)
        {
            byte[] iv = new byte[16]; // Initializing byte array for initialization vector
            Array.Copy(encryptedData, iv, 16); // Copying initialization vector from encrypted data
            return iv; // Returning extracted initialization vector
        }

        // Method to strip initialization vector from encrypted data
        private byte[] StripIV(byte[] encryptedData)
        {
            byte[] strippedData = new byte[encryptedData.Length - 16]; // Initializing byte array for stripped data
            Array.Copy(encryptedData, 16, strippedData, 0, strippedData.Length); // Copying stripped data
            return strippedData; // Returning stripped data
        }

    }
}
