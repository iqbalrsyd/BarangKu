using System;
using BCrypt.Net;

namespace BarangKu.Services
{
    public class Authenticator
    {
        private readonly Validator _validator;

        public Authenticator()
        {
            _validator = new Validator();
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string inputPassword, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedHash);
        }
    }
}