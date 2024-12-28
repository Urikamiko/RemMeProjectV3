using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для EditNoteWindow.xaml
    /// </summary>
    public partial class EditNoteWindow : Window
    {
        Note oldNote;
        NoteManager _noteManager;
        KeyManager _keyManager;
        int userID;
        List<string> keys;
        public EditNoteWindow(Note oldNote, NoteManager noteManager, KeyManager keyManager, int userID)
        {
            InitializeComponent();
            this.oldNote = oldNote;
            _noteManager = noteManager;
            _keyManager = keyManager;
            this.userID = userID;
            noteTextBox.AppendText(oldNote.Content);            
            using (ApplicationDbContext db = new())
            {
                 keys = db.KeyNotes
                   .Include(kn => kn.Key)
                   .Where(kn => kn.NoteID == oldNote.ID)
                   .Select(kn => kn.Key!.Title)
                   .ToList();
            }
            for (int i = 0; i<keys.Count; i++) {
                keysTextBox.AppendText(keys[i]);
                if (i != keys.Count - 1) {
                    keysTextBox.AppendText(", ");
                }                
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            string noteKeys = GetText(keysTextBox);
            List<string> newKeys = _keyManager.ParseKeysStr(noteKeys);
            string noteContent = GetText(noteTextBox);
            Note newNote = new Note();
            newNote.Content = noteContent;
            newNote.UserID = this.userID;
            _noteManager.Edit(oldNote.ID, keys, newKeys, newNote, _keyManager);
            Close();
        }
        public string GetText(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            string text = textRange.Text;
            return text;
        }
    }
}
