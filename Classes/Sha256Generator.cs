using System.Security.Cryptography;
using System.Text;

namespace WarehouseDesktopApp.Classes;

public class Sha256Generator
{
    public static string ComputeSHA256Hash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Преобразуем входную строку в массив байтов
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            // Вычисляем хэш для входных данных
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            // Преобразуем хэш в строку в шестнадцатеричном формате
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}