using System.Security.Cryptography;
using System.Text;

namespace Program;

public static class HashData
{

    static string HashValue(string hashValue)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Преобразование комбинированной строки в массив байтов и вычисление хеша
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(hashValue));

            // Преобразование массива байтов в строку Hex
            StringBuilder hash = new StringBuilder();
            for (int i = 0; i < hashedBytes.Length; i++)
            {
                hash.Append(hashedBytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }


    static bool VerifyHash(string inputValue, string hashedValue)
    {
        // Получение хеша
        string inputHashValue = HashValue(inputValue);

        // Сравнение хешей
        return inputHashValue == hashedValue;
    }
}
