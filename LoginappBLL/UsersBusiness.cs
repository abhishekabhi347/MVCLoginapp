using LoginappBLL.Interface;
using LoginappDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginappBLL
{
    public class UsersBusiness : IUsersBusiness
    {
        public UsersBusiness()
        {
        }

        public List<UsersDomainModel> GetAllUsers()
        {
            List<UsersDomainModel> list = new List<UsersDomainModel>
            {
                new UsersDomainModel { EmpName = "Abhi", Empid = 1 },
                new UsersDomainModel { EmpName = "Raju", Empid = 2 },
                new UsersDomainModel { EmpName = "Bannu", Empid = 3 },
                new UsersDomainModel { EmpName = "KC", Empid = 4 }
            };

            return list;
        }

        public string GetUserName(int Uid)
        {
            return "Users" + Uid;
        }
    }
}
