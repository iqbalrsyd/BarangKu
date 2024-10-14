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
            return _validator.HashPassword(password);
        }

        public bool VerifyPassword(string inputPassword, string storedHash)
        {
            return _validator.VerifyPassword(inputPassword, storedHash);
        }
    }
}