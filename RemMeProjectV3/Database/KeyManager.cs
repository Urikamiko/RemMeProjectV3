using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using RemMeProjectV3.Database;
using RemMeProjectV3.Database.Model;
using RemMeProjectV3.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RemMeProjectV3.Database
{
    public class KeyManager : IKeyManager
    {
        public void Add(Key key)
        {
            using (ApplicationDbContext db = new())
            {
                db.Keys.Add(key);
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (ApplicationDbContext db = new())
            {
                Key? toDel = db.Keys.FirstOrDefault(k => k.ID == id);
                if (toDel != null)
                {
                    db.Keys.Remove(toDel);
                    db.SaveChanges();
                }
            }
        }

        public void Edit(int oldID, Key newKey)
        {
            using (ApplicationDbContext db = new())
            {
                Key? toEdit = db.Keys.FirstOrDefault(k => k.ID == oldID);
                if (toEdit != null)
                {
                    toEdit.Title = newKey.Title;
                    db.SaveChanges();
                }
            }
        }

        public List<Key> Search (int userID, string textKey)
        {
            using (ApplicationDbContext db = new())
            {
                List<Key> foundKeys = db.Notes
                    .Include(n => n.KeyNotes)
                    .ThenInclude(kn => kn.Key)
                    .Where(n => n.UserID == userID)
                    .SelectMany(n => n.KeyNotes)
                    .Select(kn => kn.Key)
                    .Distinct()
                    .Where(k=> k.Title.Contains(textKey))
                    .ToList();
                return foundKeys;
            }
        }

        public Key Get(int id)
        {
            using (ApplicationDbContext db = new())
            {
                return db.Keys.FirstOrDefault(c => c.ID == id);
            }
        }

        public Key GetByTitle(string title)
        {
            using (ApplicationDbContext db = new())
            {
                return db.Keys.FirstOrDefault(c => c.Title == title);
            }
        }

        public List<Key> GetAll(int userID)
        {
            using (ApplicationDbContext db = new())
            {
                List<Key> userKeys = db.Notes
                    .Include(n => n.KeyNotes)
                    .ThenInclude(kn => kn.Key)
                    .Where(n => n.UserID == userID)
                    .SelectMany(n => n.KeyNotes)
                    .Select(kn => kn.Key)
                    .Distinct()
                    .ToList();
                return userKeys;
            }
        }
        public List<string> ParseKeysStr(string str) { 
            str = str.Trim();
            str = str.Replace(", ", ",");
            str = str.Replace(" ,", ",");
            str = str.Replace(" , ", ",");
            List<string> result = new List<string>();
            result = str.Split(new char[] {','}).ToList();
            return result;
        }
    }
}
