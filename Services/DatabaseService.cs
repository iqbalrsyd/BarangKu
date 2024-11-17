using BarangKu.Models;
using Npgsql;

namespace BarangKu.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString = "Host=aws-0-ap-southeast-1.pooler.supabase.com;Database=postgres;Username=postgres.adhyvkmkwhnlmysjtogs;Password=8dJXdmikvR#ntWN;SSL Mode=Disable";


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