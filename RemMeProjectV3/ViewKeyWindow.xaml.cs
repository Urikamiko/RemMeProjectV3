using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
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
    /// Логика взаимодействия для ViewKeyWindow.xaml
    /// </summary>
    public partial class ViewKeyWindow : Window
    {
        public List<Note> notesByKey = new List<Note>();
        public bool isSearch = false;
        public ViewKeyWindow(Database.Model.Key key, int userID)
        {
            InitializeComponent();
            isSearch = false;
            notesByKey.Clear();
            keyTitleLabel.Content = key.Title;
            using (ApplicationDbContext db = new())
            {
                int noteCount = db.Notes
                    .Include(n => n.KeyNotes)
                    .ThenInclude(kn => kn.Key)
                    .Where(n => n.UserID == userID)
                    .SelectMany(n => n.KeyNotes)
                    .Select(kn => kn.Key)
                    .Where(k => k.ID == key.ID)
                    .Count();
                notesByKey = db.Notes
                    .Include(n => n.KeyNotes)
                    .ThenInclude(kn => kn.Key)
                    .Where(n => n.UserID == userID)
                    .SelectMany(n => n.KeyNotes.Where(kn => kn.Key.ID == key.ID), (n, kn) => n)
                    .ToList();
                keyNoteCountLabel.Content = noteCount;
            }

        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ViewSearchButton_Click(object sender, RoutedEventArgs e)
        {
            isSearch = true;
            Close();
        }
    }
}
