using System.Security.Cryptography;
using System.Text;

namespace Program;

public static class AuthorizationHash
{



    static string HashUserAccount(string hashValue)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Преобразование комбинированной строки в массив байтов и вычисление хеша
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(hashValue));

            // Преобразование массива байтов в строку Hex
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashedBytes.Length; i++)
            {
                builder.Append(hashedBytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    // Функция проверки логина и пароля
    static bool VerifyHashAccount(string inputHashValue, string hashedValue)
    {
        // Получение хеша введенных логина и пароля
        string hashedInputUserData = HashUserAccount(inputHashValue);

        // Сравнение хешей
        return hashedInputUserData == hashedValue;
    }
}
