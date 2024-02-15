using Npgsql;

namespace Program
{
    public static class BDInteraction
    {
        #region Data

        static string connString = "Host=localhost;Port=5432;Username=postgres;Password=admin;Database=MLStartUsers";
        public static string? outputString;

        #endregion

        #region Methods

        public static NpgsqlConnection Connection()
        {
            NpgsqlConnection conn = null;
            try
            {
                conn = new NpgsqlConnection(connString);
                conn.Open();
                outputString = "Успешно подключено к базе данных PostgreSQL!";
                return conn;
            }
            catch (Exception ex)
            {
                outputString = "Ошибка: " + ex.Message;
                throw;
            }
        }

        public static void AddUser(string login, string password)
        {
            try
            {
                if (!UserCheck(login))
                {
                    using (NpgsqlConnection conn = Connection())
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand())
                        {
                            cmd.Connection = conn;
                            string hashLogin = HashData.HashValue(login);
                            string hashPassword = HashData.HashValue(password);
                            cmd.CommandText = "INSERT INTO users (login, password) VALUES (@login, @password)";
                            cmd.Parameters.AddWithValue("@login", hashLogin);
                            cmd.Parameters.AddWithValue("@password", hashPassword);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // пользователь существует
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        public static bool UserCheck(string login)
        {
            using (NpgsqlConnection conn = Connection())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    string hashLogin = HashData.HashValue(login);
                    cmd.CommandText = "SELECT COUNT(*) FROM users WHERE login = @login";
                    cmd.Parameters.AddWithValue("@login", hashLogin);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count == 1;
                }
            }
        }
        public static bool UserCheckAuthorization(string login, string password)
        {
            using (NpgsqlConnection conn = Connection())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    string hashLogin = HashData.HashValue(login);
                    string hashPassword = HashData.HashValue(password);
                    cmd.CommandText = "SELECT COUNT(*) FROM users WHERE login = @login and password = @password";
                    cmd.Parameters.AddWithValue("@login", hashLogin);
                    cmd.Parameters.AddWithValue("@password", hashPassword);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count == 1;
                }
            }
        }

        #endregion
    }
}
