using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Program;

public static class BDConnection
{

    static string  connString = "Host=myServerAddress;Port=myPort;Username=myUsername;Password=myPassword;Database=myDatabase";
    public static string? outputString;

    public static void Connection()
    {

        try
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                outputString = "Успешно подключено к базе данных PostgreSQL!";
            }
        }
        catch (Exception ex)
        {
            outputString = "Ошибка: " + ex.Message;
        }

    }

}
