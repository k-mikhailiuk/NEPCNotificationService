using System.Security.Cryptography;

namespace Common;

/// <summary>
/// Предоставляет методы для симметричного шифрования и дешифрования строк с использованием алгоритма AES.
/// </summary>
public static class Encryptor
{
    /// <summary>
    /// Симметричный ключ, используемый для шифрования и дешифрования.
    /// </summary>
    private static readonly byte[] Key = [225, 62, 18, 122, 77, 71, 7, 193, 217, 113, 61, 146, 98, 175, 82, 108];

    /// <summary>
    /// Шифрует входную строку и возвращает результат в виде строки, закодированной в Base64.
    /// </summary>
    /// <param name="plainText">Исходный текст для шифрования.</param>
    /// <returns>Зашифрованная строка в формате Base64.</returns>
    public static string Encrypt(string plainText)
    {
        var encrypted = EncryptStringToBytes_Aes(plainText, Key);
        return Convert.ToBase64String(encrypted);
    }

    /// <summary>
    /// Дешифрует строку, ранее зашифрованную методом <see cref="Encrypt"/>, и возвращает исходный текст.
    /// </summary>
    /// <param name="encryptedText">Зашифрованная строка в формате Base64.</param>
    /// <returns>Исходный расшифрованный текст.</returns>
    public static string Decrypt(string encryptedText)
    {
        var encrypted = Convert.FromBase64String(encryptedText);
        return DecryptStringFromBytes_Aes(encrypted, Key);
    }

    /// <summary>
    /// Внутренний метод шифрования текста с использованием AES в режиме CBC.
    /// Вектор инициализации (iv) генерируется автоматически и добавляется к результату.
    /// </summary>
    /// <param name="plainText">Исходный текст для шифрования.</param>
    /// <param name="key">Ключ шифрования.</param>
    /// <returns>Массив байт, содержащий iv и зашифрованные данные.</returns>
    private static byte[] EncryptStringToBytes_Aes(string plainText, byte[] key)
    {
        byte[] encrypted;
        byte[] iv;

        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = key;

            aesAlg.GenerateIV();
            iv = aesAlg.IV;

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

        var combinedIvCt = new byte[iv.Length + encrypted.Length];
        Array.Copy(iv, 0, combinedIvCt, 0, iv.Length);
        Array.Copy(encrypted, 0, combinedIvCt, iv.Length, encrypted.Length);

        return combinedIvCt;
    }

    /// <summary>
    /// Внутренний метод дешифрования данных, зашифрованных методом <see cref="EncryptStringToBytes_Aes"/>.
    /// Извлекает IV из начала массива и использует его для дешифрования остального содержимого.
    /// </summary>
    /// <param name="cipherTextCombined">Массив байт, содержащий IV и зашифрованный текст.</param>
    /// <param name="key">Ключ шифрования.</param>
    /// <returns>Расшифрованный текст.</returns>
    private static string DecryptStringFromBytes_Aes(byte[] cipherTextCombined, byte[] key)
    {
        using var aesAlg = Aes.Create();
        aesAlg.Key = key;

        var iv = new byte[aesAlg.BlockSize / 8];
        var cipherText = new byte[cipherTextCombined.Length - iv.Length];

        Array.Copy(cipherTextCombined, iv, iv.Length);
        Array.Copy(cipherTextCombined, iv.Length, cipherText, 0, cipherText.Length);

        aesAlg.IV = iv;

        aesAlg.Mode = CipherMode.CBC;

        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var msDecrypt = new MemoryStream(cipherText);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        var plaintext = srDecrypt.ReadToEnd();

        return plaintext;
    }
}