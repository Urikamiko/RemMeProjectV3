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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RemMeProjectV3
{
    /// <summary>
    /// Логика взаимодействия для ViewNoteWindow.xaml
    /// </summary>
    public partial class ViewNoteWindow : Window
    {
        public ViewNoteWindow(Note note)
        {
            InitializeComponent();
            noteContentTextBox.AppendText(note.Content);
            List<string> keys = new List<string>();
            using (ApplicationDbContext db = new())
            {
                keys = db.KeyNotes
                  .Include(kn => kn.Key)
                  .Where(kn => kn.NoteID == note.ID)
                  .Select(kn => kn.Key!.Title)
                  .ToList();
            }
            for (int i = 0; i < keys.Count; i++)
            {
                noteKeysTextBox.AppendText(keys[i]);
                if (i != keys.Count - 1)
                {
                    noteKeysTextBox.AppendText(", ");
                }
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
