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
        public async Task<UserModel> LoadProfileAsync(int userId)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand("SELECT username, email, profile_image FROM users WHERE id = @userId", conn);
            cmd.Parameters.AddWithValue("userId", userId);

            var user = new UserModel();

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                user.UserId = userId;
                user.Username = reader["username"].ToString();
                user.Email = reader["email"].ToString();

                // Convert profile_image from BYTEA to Base64 string if exists
                if (reader["profile_image"] != DBNull.Value)
                {
                    byte[] imageBytes = (byte[])reader["profile_image"];
                    user.ProfileImagePath = Convert.ToBase64String(imageBytes);
                }
                else
                {
                    user.ProfileImagePath = null;
                }
            }

            return user;
        }

        public async Task SaveProfileAsync(UserModel user)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand("UPDATE users SET username = @username, email = @Email, file_data = @file_data WHERE id = @userId", conn);

            cmd.Parameters.AddWithValue("username", user.Username);
            cmd.Parameters.AddWithValue("Email", user.Email);
            cmd.Parameters.AddWithValue("userId", user.UserId);

            // If ProfileImagePath contains data, convert Base64 to byte array for storage
            if (!string.IsNullOrEmpty(user.ProfileImagePath))
            {
                byte[] imageBytes = Convert.FromBase64String(user.ProfileImagePath);
                cmd.Parameters.AddWithValue("file_data", imageBytes);
            }
            else
            {
                cmd.Parameters.AddWithValue("file_data", DBNull.Value);
            }

            await cmd.ExecuteNonQueryAsync();
        }
    }
}