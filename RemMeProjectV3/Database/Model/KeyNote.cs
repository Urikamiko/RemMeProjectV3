using RemMeProjectV3.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemMeProjectV3.Database.Model
{
    public class KeyNote
    {
        public int ID { get; set; }

        public int KeyID { get; set; }
        public Key? Key { get; set; }
        public int NoteID { get; set; }
        public Note? Note { get; set; }

        public KeyNote() { }
    }
}
