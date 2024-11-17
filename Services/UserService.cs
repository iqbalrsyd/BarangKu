using Npgsql;
using System;

namespace BarangKu.Services
{
    public class UserService
    {
        private readonly DatabaseService _dbService;

        public UserService()
        {
            _dbService = new DatabaseService();
        }

        // Update Foto Profil
        public void UpdateProfilePicture(int userId, byte[] profilePicture)
        {
            var conn = _dbService.GetConnection();
            try
            {
                string sql = @"UPDATE users SET profile_picture = @profilePicture, modified = NOW() WHERE userid = @id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", userId);
                    cmd.Parameters.AddWithValue("profilePicture", profilePicture ?? (object)DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _dbService.CloseConnection(conn);
            }
        }

        // Mendapatkan Foto Profil
        public byte[] GetProfilePicture(int userId)
        {
            var conn = _dbService.GetConnection();
            try
            {
                string sql = @"SELECT profile_picture FROM users WHERE userid = @id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() && !reader.IsDBNull(0))
                        {
                            return (byte[])reader["profile_picture"];
                        }
                    }
                }
            }
            finally
            {
                _dbService.CloseConnection(conn);
            }
            return null;
        }
    }
}
