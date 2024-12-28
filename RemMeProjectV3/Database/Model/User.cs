using RemMeProjectV3.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemMeProjectV3.Database.Model
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public HashSet<Note>? Notes { get; set; }
        public User() { }
    }
}
