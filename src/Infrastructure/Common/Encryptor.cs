using System.Security.Cryptography;

namespace Common;

/// <summary>
/// Шифроввание/Дешифрование строк
/// </summary>
public static class Encryptor
{
    private static readonly byte[] Key = new byte[16]
        { 225, 62, 18, 122, 77, 71, 7, 193, 217, 113, 61, 146, 98, 175, 82, 108 };

    /// <summary>
    /// Шифрование
    /// </summary>
    public static string Encrypt(string plainText)
    {
        var encrypted = EncryptStringToBytes_Aes(plainText, Key);
        return Convert.ToBase64String(encrypted);
    }


    /// <summary>
    /// Дешифрование
    /// </summary>
    public static string Decrypt(string encryptedText)
    {
        var encrypted = Convert.FromBase64String(encryptedText);
        return DecryptStringFromBytes_Aes(encrypted, Key);
    }


    static byte[] EncryptStringToBytes_Aes(string plainText, byte[] key)
    {
        byte[] encrypted;
        byte[] IV;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;

            aesAlg.GenerateIV();
            IV = aesAlg.IV;

            aesAlg.Mode = CipherMode.CBC;

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption. 
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }

                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        var combinedIvCt = new byte[IV.Length + encrypted.Length];
        Array.Copy(IV, 0, combinedIvCt, 0, IV.Length);
        Array.Copy(encrypted, 0, combinedIvCt, IV.Length, encrypted.Length);

        // Return the encrypted bytes from the memory stream. 
        return combinedIvCt;
    }

    static string DecryptStringFromBytes_Aes(byte[] cipherTextCombined, byte[] key)
    {
        // Declare the string used to hold 
        // the decrypted text. 
        string plaintext = null;

        // Create an Aes object 
        // with the specified key and IV. 
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;

            byte[] IV = new byte[aesAlg.BlockSize / 8];
            byte[] cipherText = new byte[cipherTextCombined.Length - IV.Length];

            Array.Copy(cipherTextCombined, IV, IV.Length);
            Array.Copy(cipherTextCombined, IV.Length, cipherText, 0, cipherText.Length);

            aesAlg.IV = IV;

            aesAlg.Mode = CipherMode.CBC;

            // Create a decrytor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption. 
            using (var msDecrypt = new MemoryStream(cipherText))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return plaintext;
    }
}