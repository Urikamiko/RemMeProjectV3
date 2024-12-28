using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RemMeProjectV3.Database;
using RemMeProjectV3.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RemMeProjectV3
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        KeyManager _keyManager;
        NoteManager _noteManager;
        int _userID;
        public AddWindow(NoteManager noteManager, KeyManager keyManager, int userID)
        {
            InitializeComponent();
            _keyManager = keyManager;
            _noteManager = noteManager;
            _userID = userID;
            keysTextBox.Document.Blocks.Clear();
            noteTextBox.Document.Blocks.Clear();
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            string keysContent = GetText(keysTextBox);
            List<string> keys = _keyManager.ParseKeysStr(keysContent);
            foreach (string key in keys)
            {
                if (_keyManager.GetByTitle(key) == null)
                {
                    RemMeProjectV3.Database.Model.Key newkey = new Database.Model.Key();
                    newkey.Title = key;
                    _keyManager.Add(newkey);
                }                
            }
            string noteContent = GetText(noteTextBox);
            if (noteContent != "" && noteContent!="\r\n") {
                Note newNote = new Note();
                newNote.Content = noteContent;
                newNote.UserID = _userID;
                _noteManager.Add(newNote);
                foreach (string key in keys)
                {
                    KeyNote keyNote = new KeyNote();
                    keyNote.NoteID = newNote.ID;
                    keyNote.KeyID = _keyManager.GetByTitle(key).ID;
                    using (ApplicationDbContext db = new())
                    {
                        db.KeyNotes.Add(keyNote);
                        db.SaveChanges();
                    }
                }
            }
            Close();
        }
        public string GetText(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            string text = textRange.Text;
            return text;
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
