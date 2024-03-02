using Npgsql;

namespace Program;

public static class BdManager
{


    public static string GetConnection()
    {
        NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder();
        builder.Host = ConfigManager.GetBdConfig("Host");
        builder.Port = Convert.ToInt32(ConfigManager.GetBdConfig("Port"));
        builder.Username = ConfigManager.GetBdConfig("Username");
        builder.Password = ConfigManager.GetBdConfig("Password");
        builder.Database = ConfigManager.GetBdConfig("DatabaseName");
        builder.SearchPath = ConfigManager.GetBdConfig("SchemaName");

        string connString = builder.ConnectionString;

        if (!DatabaseExists(builder.ConnectionString))
        {
            CreateDatabase(builder.ConnectionString);
        }

        if (!SchemaExists(builder.ConnectionString))
        {
            CreateSchema(builder.ConnectionString);
        }

        if (!TableExists(builder.ConnectionString))
        {
            CreateTable(builder.ConnectionString);
        }


        return connString;
    }

    private static bool DatabaseExists(string connectionString)
    {
        using (var conn = new NpgsqlConnection(connectionString))
        {
            try { conn.Open(); }
            catch (Exception e) { return false; }
            using (var cmd = new NpgsqlCommand("SELECT 1 FROM pg_database WHERE datname = @dbName", conn))
            {
                cmd.Parameters.AddWithValue("@dbName", conn.Database);
                return cmd.ExecuteScalar() != null;
            }
        }
    }

    private static void CreateDatabase(string connectionString)
    {
        var dbName = new NpgsqlConnectionStringBuilder(connectionString).Database;
        var templateConnectionString = new NpgsqlConnectionStringBuilder(connectionString)
        {
            Database = "postgres"
        }.ConnectionString;
        using (var templateConn = new NpgsqlConnection(templateConnectionString))
        {
            templateConn.Open();
            using (var cmd = new NpgsqlCommand($"CREATE DATABASE \"{dbName}\"", templateConn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }

    private static bool SchemaExists(string connectionString)
    {
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("SELECT 1 FROM information_schema.schemata WHERE schema_name = @schemaName", conn))
            {
                cmd.Parameters.AddWithValue("@schemaName", new NpgsqlConnectionStringBuilder(connectionString).SearchPath);
                return cmd.ExecuteScalar() != null;
            }
        }
    }

    private static void CreateSchema(string connectionString)
    {
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand($"CREATE SCHEMA {new NpgsqlConnectionStringBuilder(connectionString).SearchPath}", conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }

    private static bool TableExists(string connectionString)
    {
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("SELECT 1 FROM information_schema.tables WHERE table_schema = @schemaName AND table_name = @tableName", conn))
            {
                var builder = new NpgsqlConnectionStringBuilder(connectionString);
                cmd.Parameters.AddWithValue("@schemaName", builder.SearchPath);
                cmd.Parameters.AddWithValue("@tableName", "users");
                return cmd.ExecuteScalar() != null;
            }
        }
    }

    private static void CreateTable(string connectionString)
    {
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("CREATE TABLE users (login text NOT NULL, password text NOT NULL)", conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }

}

