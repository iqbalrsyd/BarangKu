using System;

namespace BarangKu.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Telephone { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string HashedPassword { get; set; }
        public string Address { get; set; }
        public string Language { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}