using System;
using System.Security.Cryptography;
using System.Text;

namespace BarangKu
{
    public class User
    {
        public int UserId { get; private set; }
        public string Username { get; set; }
        public string HashedPassword { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }


        private Validator _validator;

        public User()
        {
            _validator = new Validator();
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }

        public void Register(string username, string password, string firstName, string lastName, string telephone)
        {
            Username = username;
            HashedPassword = _validator.HashPassword(password);
            FirstName = firstName;
            LastName = lastName;
            Telephone = telephone;
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }


        public bool Login(string username, string password)
        {
            if (Username == username)
            {
                return _validator.VerifyPassword(password, HashedPassword);
            }
            return false;
        }


        public void UpdateProfile(string firstName, string lastName, string telephone)
        {
            FirstName = firstName;
            LastName = lastName;
            Telephone = telephone;
            Modified = DateTime.Now;
        }


        public void DeleteAccount()
        {
            // In a real application, this would include deletion logic.
            Username = null;
            HashedPassword = null;
            FirstName = null;
            LastName = null;
            Telephone = null;
            Modified = DateTime.Now;
        }
    }

    public class Validator
    {
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }


        public bool VerifyPassword(string inputPassword, string storedHash)
        {
            string hashedInput = HashPassword(inputPassword);
            return hashedInput.Equals(storedHash);
        }
    }


    // Class Authenticator
    public class Authenticator
    {
        private Validator _validator;


        public Authenticator()
        {
            _validator = new Validator();
        }


        public string HashPassword(string password)
        {
            return _validator.HashPassword(password);
        }


        public bool VerifyPassword(string inputPassword, string storedHash)
        {
            return _validator.VerifyPassword(inputPassword, storedHash);
        }
    }
}
