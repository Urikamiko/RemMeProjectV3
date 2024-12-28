using Microsoft.VisualBasic.ApplicationServices;
using RemMeProjectV3.Database;
using RemMeProjectV3.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemMeProjectV3.Database
{
    public class UserManager : IUserManager
    {
        public void Add(Model.User user)
        {
            using (ApplicationDbContext db = new())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
        public List<Model.User> GetAll()
        {
            using (ApplicationDbContext db = new())
            {
                return db.Users.ToList();
            }
        }

        public Model.User GetById(int id)
        {
            using (ApplicationDbContext db = new())
            {
                return db.Users.FirstOrDefault(u => u.ID == id);
            }
        }

        public Model.User GetByName(string name)
        {
            using (ApplicationDbContext db = new())
            {
                return db.Users.FirstOrDefault(u => u.UserName == name);
            }
        }
    }
}
