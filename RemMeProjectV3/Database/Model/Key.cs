using RemMeProjectV3.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemMeProjectV3.Database.Model
{
    public class Key
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public HashSet<KeyNote>? KeyNotes { get; set; }

        public Key() { }

    }
}
