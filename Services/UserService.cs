using BarangKu.Models;
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

        public void UpdateUser(UserModel user)
        {
            var conn = _dbService.GetConnection();
            try
            {
                string sql = @"UPDATE users SET 
                            username = @username, 
                            firstname = @firstName,
                            lastname = @lastName,
                            telephone = @telephone,
                            email = @email,
                            address = @address,
                            language = @language,
                            profile_picture = @profilePicture,
                            modified = NOW()
                       WHERE userid = @id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", user.UserId);
                    cmd.Parameters.AddWithValue("username", user.Username);
                    cmd.Parameters.AddWithValue("firstName", user.FirstName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("lastName", user.LastName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("telephone", user.Telephone ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("email", user.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("address", user.Address ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("language", user.Language ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("profilePicture", user.ProfilePicture ?? (object)DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _dbService.CloseConnection(conn);
            }
        }

        public UserModel GetUserById(int userId)
        {
            var conn = _dbService.GetConnection();
            try
            {
                string sql = @"SELECT userid, username, firstname, lastname, telephone, email, address, language, profile_picture 
                       FROM users 
                       WHERE userid = @id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserModel
                            {
                                UserId = reader.GetInt32(reader.GetOrdinal("userid")),
                                Username = reader.IsDBNull(reader.GetOrdinal("username")) ? null : reader.GetString(reader.GetOrdinal("username")),
                                FirstName = reader.IsDBNull(reader.GetOrdinal("firstname")) ? null : reader.GetString(reader.GetOrdinal("firstname")),
                                LastName = reader.IsDBNull(reader.GetOrdinal("lastname")) ? null : reader.GetString(reader.GetOrdinal("lastname")),
                                Telephone = reader.IsDBNull(reader.GetOrdinal("telephone")) ? null : reader.GetString(reader.GetOrdinal("telephone")),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),
                                Address = reader.IsDBNull(reader.GetOrdinal("address")) ? null : reader.GetString(reader.GetOrdinal("address")),
                                Language = reader.IsDBNull(reader.GetOrdinal("language")) ? null : reader.GetString(reader.GetOrdinal("language")),
                                ProfilePicture = reader.IsDBNull(reader.GetOrdinal("profile_picture")) ? null : (byte[])reader["profile_picture"]
                            };
                        }
                    }
                }
            }
            finally
            {
                _dbService.CloseConnection(conn);
            }

            return null; // Return null jika user tidak ditemukan
        }


    }
}
