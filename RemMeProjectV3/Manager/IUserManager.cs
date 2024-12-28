using RemMeProjectV3.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemMeProjectV3.Manager
{
    public interface IUserManager
    {
        void Add(User user);
        List<User> GetAll();
        User GetById(int id);
        User GetByName(string name);
    }
}
