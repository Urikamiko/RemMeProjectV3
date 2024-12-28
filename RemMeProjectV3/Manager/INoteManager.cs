using RemMeProjectV3.Database;
using RemMeProjectV3.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemMeProjectV3.Manager
{
    public interface INoteManager
    {
        void Add(Note note);
        void Edit(int oldId, List<string> oldKeysList, List<string> newKeysList, Note newNote, KeyManager keyManager);
        List<Note> Search(int userID, string searchText);
        void Remove(int id);
        List<Note> GetAll(int userID);
        Note Get(int id);
    }
}
