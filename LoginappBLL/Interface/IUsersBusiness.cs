using LoginappDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginappBLL.Interface
{
    public interface IUsersBusiness
    {
        string GetUserName(int Uid);
        List<UsersDomainModel> GetAllUsers();
    }
}
