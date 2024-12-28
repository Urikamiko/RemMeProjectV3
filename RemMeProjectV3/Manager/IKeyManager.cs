using RemMeProjectV3.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RemMeProjectV3.Manager
{
    public interface IKeyManager
    {
        void Add(Key key);
        void Edit(int oldID, Key newKey);
        List<Key> Search(int userID, string textKey);
        void Delete(int id);
        List<Key> GetAll(int userID);
        Key Get(int id);
    }
}
