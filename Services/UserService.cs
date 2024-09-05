using Npgsql;
using System;
using BarangKu.Models;

namespace BarangKu.Services
{
    public class UserService
    {
        private readonly DatabaseService _dbService;

        public UserService()
        {
            _dbService = new DatabaseService();
        }

        public User GetUserById(int id)
        {
            User user = null;
            var conn = _dbService.GetConnection();

            try
            {
                string sql = @"SELECT * FROM users WHERE user_id = @id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                UserId = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                FirstName = reader.GetString(2),
                                LastName = reader.GetString(3),
                                Telephone = reader.GetString(4),
                                Created = reader.GetDateTime(5),
                                Modified = reader.GetDateTime(6)
                            };
                        }
                    }
                }
            }
            finally
            {
                _dbService.CloseConnection();
            }

            return user;
        }

        public void UpdateUser(int userId, string name, string alamat, string noHandphone)
        {
            var conn = _dbService.GetConnection();

            try
            {
                string sql = @"select * from st_update(:_id,:_name,:_alamat,:_no_handphone)";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("_id", userId);
                    cmd.Parameters.AddWithValue("_name", name);
                    cmd.Parameters.AddWithValue("_alamat", alamat);
                    cmd.Parameters.AddWithValue("_no_handphone", noHandphone);

                    if ((int)cmd.ExecuteScalar() == 1)
                    {
                        // Berhasil memperbarui
                    }
                }
            }
            finally
            {
                _dbService.CloseConnection();
            }
        }
    }
}
