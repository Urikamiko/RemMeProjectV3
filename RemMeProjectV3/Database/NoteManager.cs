using RemMeProjectV3.Database.Model;
using RemMeProjectV3.Database;
using RemMeProjectV3.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;
using System.Collections.Immutable;
using Microsoft.VisualBasic.ApplicationServices;

namespace RemMeProjectV3.Database
{
    public class NoteManager : INoteManager
    {
        public void Add(Note note)
        {
            using (ApplicationDbContext db = new())
            {
                db.Notes.Add(note);
                db.SaveChanges();
            }
        }

        public void Edit(int oldId, List<string> oldKeysList, List<string> newKeysList, Note newNote, KeyManager keyManager)
        {
            using (ApplicationDbContext db = new())
            {
                Note? toEdit = db.Notes.FirstOrDefault(n => n.ID == oldId);
                if (toEdit != null)
                {
                    for (int i = 0; i < oldKeysList.Count; i++)
                    {
                        if (newKeysList.Contains(oldKeysList[i]))
                        {
                            continue;
                        }
                        else
                        {
                            KeyNote kn = db.KeyNotes.FirstOrDefault(o=>o.KeyID == keyManager.GetByTitle(oldKeysList[i]).ID && o.NoteID==oldId);                            
                            db.KeyNotes.Remove(kn);
                            db.SaveChanges();
                        }
                    }

                    for (int i = 0; i < newKeysList.Count; i++)
                    {
                        if (oldKeysList.Contains(newKeysList[i]))
                        {
                            continue;
                        }
                        else
                        {
                            if (keyManager.GetByTitle(newKeysList[i]) != null)
                            {
                                KeyNote kn = new KeyNote();
                                kn.NoteID = oldId;
                                kn.KeyID = keyManager.GetByTitle(newKeysList[i]).ID;
                                db.KeyNotes.Add(kn);
                                db.SaveChanges();
                            }
                            else
                            {
                                Key newKey = new Key();
                                newKey.Title = newKeysList[i];
                                db.Keys.Add(newKey);
                                db.SaveChanges();
                                KeyNote kn = new KeyNote();
                                kn.NoteID = oldId;
                                kn.KeyID = newKey.ID;
                                db.KeyNotes.Add(kn);
                                db.SaveChanges();
                            }
                        }
                    }

                    toEdit.Content = newNote.Content;
                    toEdit.UserID = newNote.UserID;
                    db.SaveChanges();
                }
            }
        }
        public void Remove(int id)
        {
            using (ApplicationDbContext db = new())
            {
                Note? toDel = db.Notes.FirstOrDefault(n => n.ID == id);
                if (toDel != null)
                {
                    db.Remove(toDel);
                    db.SaveChanges();
                }
            }
        }

        public List<Note> Search(int userID, string searchText) {
            using (ApplicationDbContext db = new())
            {
                List<Note> userNotes = db.Notes
                    .Where(n => n.UserID == userID && n.Content.Contains(searchText))
                    .ToList();
                return userNotes;
            }
        }

        public Note Get(int id)
        {
            using (ApplicationDbContext db = new())
            {
                return db.Notes.FirstOrDefault(n => n.ID == id);
            }
        }

        public List<Note> GetAll(int userID)
        {
            using (ApplicationDbContext db = new())
            {
                List<Note> userNotes = db.Notes
                    .Where(n => n.UserID == userID)
                    .ToList();
                return userNotes;
            }
        }
    }
}
