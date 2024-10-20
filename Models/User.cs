using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarangKu
{
    public class User
    {
        private Employee _employee;

        public string LoginName
        {
            get { return _employee?.LoginName; }
            set { if (_employee != null) _employee.LoginName = value; }
        }

        public string Password
        {
            get { return _employee?.Password; }
            set { if (_employee != null) _employee.Password = value; }
        }

        public int EmployeeID
        {
            get { return _employee?.EmployeeID ?? 0; }
        }

        public User()
        {
            _employee = new Employee();
        }

        public bool Login(string loginName, string password)
        {
            return _employee.Login(loginName, password);
        }

        private class Employee
        {
            private int _empID;
            private string _loginName;
            private string _password;
            private int _securityLevel;

            public int EmployeeID => _empID;

            public string LoginName
            {
                get { return _loginName; }
                set { _loginName = value; }
            }

            public string Password
            {
                get { return _password; }
                set { _password = value; }
            }

            public int SecurityLevel => _securityLevel;

            public bool Login(string loginName, string password)
            {
                if (loginName == "Nadia" && password == "wkwk")
                {
                    _empID = 1;
                    _securityLevel = 2;
                    return true;
                }
                else if (loginName == "Iqbal" && password == "haha")
                {
                    _empID = 2;
                    _securityLevel = 4;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
