using Npgsql;

namespace BarangKu.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString = "Host=barangku.postgres.database.azure.com;Database=barangku;Username=postgres;Password=admin123!;SSL Mode=Require;";

        public NpgsqlConnection GetConnection()
        {
            var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            return conn;
        }

        public void CloseConnection(NpgsqlConnection conn)
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
