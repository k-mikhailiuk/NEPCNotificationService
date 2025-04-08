using System.Security.Cryptography;

namespace Common;

/// <summary>
/// Шифроввание/Дешифрование строк
/// </summary>
public static class Encryptor
{
    private static readonly byte[] Key = [225, 62, 18, 122, 77, 71, 7, 193, 217, 113, 61, 146, 98, 175, 82, 108];

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

        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = key;

            aesAlg.GenerateIV();
            IV = aesAlg.IV;

            aesAlg.Mode = CipherMode.CBC;

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }

                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        var combinedIvCt = new byte[IV.Length + encrypted.Length];
        Array.Copy(IV, 0, combinedIvCt, 0, IV.Length);
        Array.Copy(encrypted, 0, combinedIvCt, IV.Length, encrypted.Length);

        return combinedIvCt;
    }

    static string DecryptStringFromBytes_Aes(byte[] cipherTextCombined, byte[] key)
    {
        string plaintext;

        using var aesAlg = Aes.Create();
        aesAlg.Key = key;

        var IV = new byte[aesAlg.BlockSize / 8];
        var cipherText = new byte[cipherTextCombined.Length - IV.Length];

        Array.Copy(cipherTextCombined, IV, IV.Length);
        Array.Copy(cipherTextCombined, IV.Length, cipherText, 0, cipherText.Length);

        aesAlg.IV = IV;

        aesAlg.Mode = CipherMode.CBC;

        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var msDecrypt = new MemoryStream(cipherText);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        plaintext = srDecrypt.ReadToEnd();

        return plaintext;
    }
}