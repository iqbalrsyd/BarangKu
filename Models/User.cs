using BarangKu.Services;
using Npgsql;
using System;

namespace BarangKu.Models
{
    public class User
    {
        public int userid { get; set; }
        public string username { get; set; }
        public string hashedpassword { get; set; }


        public bool Login(string username, string hashedpassword)
        {
            DatabaseService dbService = new DatabaseService();
            bool isAuthenticated = false;

            using (var conn = dbService.GetConnection())
            {
                string query = "SELECT * FROM Users WHERE username = @username AND hashedpassword = @hashedpassword";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("hashedpassword", hashedpassword);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Set properties from database results
                            userid = Convert.ToInt32(reader["userid"]);
                            username = reader["username"].ToString();
                            hashedpassword = reader["hashedpassword"].ToString();

                            isAuthenticated = true; // Login berhasil
                        }
                    }
                }
            }
            return isAuthenticated;
        }
    }
}
