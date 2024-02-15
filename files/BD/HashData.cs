using System.Security.Cryptography;
using System.Text;

namespace Program;

public static class HashData
{
    #region Methods

    public static string HashValue(string value)
    {
        SHA256 sha256 = SHA256.Create();
        // Преобразование комбинированной строки в массив байтов и вычисление хеша
        byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));

        // Преобразование массива байтов в строку Hex(16-тиричная строка)
        StringBuilder hash = new StringBuilder();
        for (int i = 0; i < hashedBytes.Length; i++)
        {
            hash.Append(hashedBytes[i].ToString("x2"));
        }
        sha256.Dispose();
        return hash.ToString();
    }

    #endregion
}
