using RemMeProjectV3.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemMeProjectV3.Database.Model
{
    public class Note
    {
        public int ID { get; set; }
        public string Content { get; set; } = string.Empty;

        public int UserID { get; set; }
        public User? User { get; set; }

        public HashSet<KeyNote>? KeyNotes { get; set; }

        public Note() { }

    }
}
