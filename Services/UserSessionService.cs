using BarangKu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarangKu.Services
{
    public class UserSessionService
    {
        private static UserSessionService _instance;
        public UserModel User { get; set; } 
        public SellerModel Seller { get; set; }

        private UserSessionService() { }

        public static UserSessionService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserSessionService();
                return _instance;
            }
        }
    }
}
