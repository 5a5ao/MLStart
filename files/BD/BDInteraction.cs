using Npgsql;

namespace Program;

public static class BDInteraction 
{

    #region Data

    static string connString = "Host=localhost;Port=5432;Username=postgres;Password=admin;Database=MLStartUsers";
    public static string? outputString;
    public static NpgsqlCommand cmd = new NpgsqlCommand();

    #endregion

    #region Methods

    public static NpgsqlConnection Connection()
    {

        try
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            conn.Open();
            outputString = "Успешно подключено к базе данных PostgreSQL!";
            return conn;
        }
        catch (Exception ex)
        {
            outputString = "Ошибка: " + ex.Message;
            return null;
        }

    }

    public static void AddUser(string login, string password)
    {
        try
        {
            if (UserCheck(login) == false)
            {
                Connection();
                string hashLogin = HashData.HashValue(login);
                string hashPassword = HashData.HashValue(password);
                cmd.CommandText = "INSERT INTO users (login, password) VALUES (@login, @password)";
                cmd.Parameters.AddWithValue("@login", hashLogin);
                cmd.Parameters.AddWithValue("@password", hashPassword);
                cmd.ExecuteNonQuery();
            }
            else { 
                //ползовтаель сущесвтует
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }

    public static bool UserCheck(string login)
    {
        Connection();
        {
            string hashLogin = HashData.HashValue(login);
            cmd.CommandText = "SELECT 1 FROM users WHERE login = @login LIMIT 1";
            cmd.Parameters.AddWithValue("@login", hashLogin);
            int count = (int)cmd.ExecuteScalar();
            return count == 1;
        }

    }

    #endregion

}
