using System;
using System.Windows;
using BCrypt.Net;
using Npgsql;

namespace BarangKu.Services
{
    public class Authenticator
    {
        private readonly Validator _validator;
        private readonly DatabaseService _dbService;

        public Authenticator()
        {
            _validator = new Validator();
            _dbService = new DatabaseService();
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string inputPassword, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedHash);
        }

        private bool IsUserSeller(int userId)
        {
            bool isSeller = false;
            var conn = _dbService.GetConnection();

            try
            {
                string query = "SELECT COUNT(1) FROM seller WHERE userid = @userId";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("userId", userId);
                    isSeller = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking seller status: {ex.Message}");
            }
            finally
            {
                _dbService.CloseConnection(conn);
            }

            return isSeller;
        }

        public bool AccessStorePage()
        {
            int userId = UserSessionService.Instance.User.UserId;

            if (IsUserSeller(userId))
            { 
                return true;

            }
            else
            {
                MessageBox.Show("Toko Anda belum dibuat", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
        }
    }
}