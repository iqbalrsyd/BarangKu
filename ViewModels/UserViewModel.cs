using BarangKu.Models;
using BarangKu.Services;
using Npgsql;
using System.ComponentModel;

namespace BarangKu.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private UserModel _user;
        private readonly Authenticator _authenticator;
        private readonly DatabaseService _dbService;
        public UserModel User { get; set; }

        public UserViewModel()
        {
            _user = new UserModel();
            _authenticator = new Authenticator();
           _dbService = new DatabaseService();
        }

        //public UserModel User
        //{
        //    get { return _user; }
        //    set
        //    {
        //        _user = value;
        //        OnPropertyChanged("User");
        //    }
        //}

        public bool Login(string username, string password)
        {
            var conn = _dbService.GetConnection();
            bool loginSuccessful = false;

            try
            {
                string login = "SELECT userid, hashedpassword FROM users WHERE username = @username";
                using (var cmd = new NpgsqlCommand(login, conn))
                {
                    cmd.Parameters.AddWithValue("username", username);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int userId = reader.GetInt32(0);
                            string storedHash = reader.GetString(1);

                            // Use Authenticator to verify the password
                            if (_authenticator.VerifyPassword(password, storedHash))
                            {
                                loginSuccessful = true;
                                User = new UserModel { UserId = userId, Username = username };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                _dbService.CloseConnection(conn);
            }
            return loginSuccessful;
        }

        public UserModel Register(string username, string password, string firstName, string lastName, string email, string telephone)
        {
            UserModel user = null;
            var conn = _dbService.GetConnection();

            try
            {
                int userid;
                bool isUnique = false;

                do
                {
                    Random random = new Random();
                    userid = random.Next(1000, 10000);

                    string checkUserid = "SELECT COUNT(1) FROM users WHERE userid = @userid";
                    using (var checkCmd = new NpgsqlCommand(checkUserid, conn))
                    {
                        checkCmd.Parameters.AddWithValue("userid", userid);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        isUnique = (count == 0);
                    }
                } while (!isUnique);

                string hashedPassword = _authenticator.HashPassword(password);

                string create = @"INSERT INTO users (userid, username, hashedpassword, firstname, lastname, email, telephone, created, modified) 
                              VALUES (@userid, @username, @hashedPassword, @firstname, @lastname, @email, @telephone, NOW(), NOW())";

                using (var cmd = new NpgsqlCommand(create, conn))
                {
                    cmd.Parameters.AddWithValue("userid", userid);
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("hashedpassword", hashedPassword);
                    cmd.Parameters.AddWithValue("firstname", firstName);
                    cmd.Parameters.AddWithValue("lastname", lastName);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("telephone", telephone);

                    cmd.ExecuteNonQuery();

                    user = new UserModel
                    {
                        UserId = userid,
                        Username = username,
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        Telephone = telephone,
                        Created = DateTime.Now,
                        Modified = DateTime.Now
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                _dbService.CloseConnection(conn);
            }
            return user;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
