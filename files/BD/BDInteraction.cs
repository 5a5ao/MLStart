using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Program;

public static class BDInteraction
{

    static string connString = "Host=localhost;Port=5432;Username=postgres;Password=admin;Database=MLStartUsers";
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
