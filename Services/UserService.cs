using System;
using BarangKu.Models;
using BarangKu.ViewModels;
using Npgsql;

namespace BarangKu.Services
{
    public class UserService
    {
        private readonly DatabaseService _dbService;

        public UserService()
        {
            _dbService = new DatabaseService();
        }

        public UserModel GetUserById(int id)
        {
            UserModel user = null;
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
                            user = new UserModel
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
                _dbService.CloseConnection(conn);
            }

            return user;
        }
    }
}
