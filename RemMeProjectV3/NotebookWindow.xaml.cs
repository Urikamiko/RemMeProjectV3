using RemMeProjectV3.Database;
using RemMeProjectV3.Database.Model;
using RemMeProjectV3.Manager;
using RemMeProjectV3.WindowActions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
    /// Логика взаимодействия для NotebookWindow.xaml
    /// </summary>
    public partial class NotebookWindow : Window
    {
        private int currentUserID;
        KeyManager keyManager;
        NoteManager noteManager;
        Pagination<Database.Model.Key> paginationKey;
        Pagination<Note> paginationNote;

        public NotebookWindow(int userId)
        {
            InitializeComponent();
            currentUserID = userId;
            keyManager = new KeyManager();
            noteManager = new NoteManager();
            paginationKey = new Pagination<Database.Model.Key>();
            paginationNote = new Pagination<Note>();
            RefreshTable();
        }

        private void AddMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow(noteManager, keyManager, currentUserID);
            addWindow.ShowDialog();
            RefreshTable();
        }

        private void ViewMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (MainField.SelectedIndex == 0)
            {
                if (KeysListView.SelectedItem is Database.Model.Key selectedKey)
                {
                    ViewKeyWindow viewWindow = new ViewKeyWindow(selectedKey, currentUserID);
                    viewWindow.ShowDialog();
                    if (viewWindow.isSearch)
                    {
                        MainField.SelectedIndex = 1;
                        RefreshTable(viewWindow.notesByKey);
                    }
                }
            }
            else if (MainField.SelectedIndex == 1)
            {
                if (NotesListView.SelectedItem is Note selectedNote)
                {
                    ViewNoteWindow viewWindow = new ViewNoteWindow(selectedNote);
                    viewWindow.ShowDialog();
                }
            }
        }

        private void RemoveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (MainField.SelectedIndex == 0)
            {
                MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить этот ключ?",
                    "Удалить ключ",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    if (KeysListView.SelectedItem is Database.Model.Key selectedKey)
                    {
                        keyManager.Delete(selectedKey.ID);
                    }
                    RefreshTable();
                }
            }
            else if (MainField.SelectedIndex == 1)
            {
                MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить эту заметку?",
                    "Удалить заметку",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    if (NotesListView.SelectedItem is Note selectedNote)
                    {
                        noteManager.Remove(selectedNote.ID);
                    }
                    RefreshTable();
                }
            }
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (MainField.SelectedIndex == 0)
            {
                if (KeysListView.SelectedItem is Database.Model.Key selectedKey)
                {
                    EditKeyWindow editKeyWindow = new EditKeyWindow(selectedKey.ID, keyManager);
                    editKeyWindow.ShowDialog();
                    RefreshTable();
                }
            }
            else if (MainField.SelectedIndex == 1)
            {
                if (NotesListView.SelectedItem is Note selectedNote)
                {
                    EditNoteWindow editNoteWindow = new EditNoteWindow(selectedNote, noteManager, keyManager, currentUserID);
                    editNoteWindow.ShowDialog();
                    RefreshTable();
                }
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (MainField.SelectedIndex == 0)
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    List<Database.Model.Key> foundKeys = keyManager.Search(currentUserID, KeyTextBox.Text);
                    RefreshTable(foundKeys);
                }
            }
            else if (MainField.SelectedIndex == 1)
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    List<Note> foundNotes = noteManager.Search(currentUserID, NoteTextBox.Text);
                    RefreshTable(foundNotes);
                }
            }
        }

        private void RefreshTable()
        {
            KeysListView.ItemsSource = null;
            KeysListView.Items.Clear();
            KeysListView.ItemsSource = paginationKey.GetItems(keyManager.GetAll(currentUserID));
            NotesListView.ItemsSource = null;
            NotesListView.Items.Clear();
            NotesListView.ItemsSource = paginationNote.GetItems(noteManager.GetAll(currentUserID));
            keyCurPageTextBox.Text = paginationKey.currentPage.ToString();
            noteCurPageTextBox.Text = paginationNote.currentPage.ToString();
            keyMaxPageLabel.Content = paginationKey.maxPage.ToString();
            noteMaxPageLabel.Content = paginationNote.maxPage.ToString();
            if (!paginationKey.isPrev())
            {
                keyPrevPageButton.IsEnabled = false;
            }
            else
            {
                keyPrevPageButton.IsEnabled = true;
            }
            if (!paginationKey.isNext())
            {
                keyNextPageButton.IsEnabled = false;
            }
            else
            {
                keyNextPageButton.IsEnabled = true;
            }

            if (!paginationNote.isPrev())
            {
                notePrevPageButton.IsEnabled = false;
            }
            else
            {
                notePrevPageButton.IsEnabled = true;
            }
            if (!paginationNote.isNext())
            {
                noteNextPageButton.IsEnabled = false;
            }
            else
            {
                noteNextPageButton.IsEnabled = true;
            }
        }

        private void RefreshTable(List<Database.Model.Key> keys)
        {
            KeysListView.ItemsSource = null;
            KeysListView.Items.Clear();
            KeysListView.ItemsSource = paginationKey.GetItems(keys);
            NotesListView.ItemsSource = null;
            NotesListView.Items.Clear();
            NotesListView.ItemsSource = paginationNote.GetItems(noteManager.GetAll(currentUserID));
            keyCurPageTextBox.Text = paginationKey.currentPage.ToString();
            noteCurPageTextBox.Text = paginationNote.currentPage.ToString();
            keyMaxPageLabel.Content = paginationKey.maxPage.ToString();
            noteMaxPageLabel.Content = paginationNote.maxPage.ToString();
            if (!paginationKey.isPrev())
            {
                keyPrevPageButton.IsEnabled = false;
            }
            else
            {
                keyPrevPageButton.IsEnabled = true;
            }
            if (!paginationKey.isNext())
            {
                keyNextPageButton.IsEnabled = false;
            }
            else
            {
                keyNextPageButton.IsEnabled = true;
            }

            if (!paginationNote.isPrev())
            {
                notePrevPageButton.IsEnabled = false;
            }
            else
            {
                notePrevPageButton.IsEnabled = true;
            }
            if (!paginationNote.isNext())
            {
                noteNextPageButton.IsEnabled = false;
            }
            else
            {
                noteNextPageButton.IsEnabled = true;
            }
        }

        private void RefreshTable(List<Note> notes)
        {
            KeysListView.ItemsSource = null;
            KeysListView.Items.Clear();
            KeysListView.ItemsSource = paginationKey.GetItems(keyManager.GetAll(currentUserID));
            NotesListView.ItemsSource = null;
            NotesListView.Items.Clear();
            NotesListView.ItemsSource = paginationNote.GetItems(notes);
            keyCurPageTextBox.Text = paginationKey.currentPage.ToString();
            noteCurPageTextBox.Text = paginationNote.currentPage.ToString();
            keyMaxPageLabel.Content = paginationKey.maxPage.ToString();
            noteMaxPageLabel.Content = paginationNote.maxPage.ToString();
            if (!paginationKey.isPrev())
            {
                keyPrevPageButton.IsEnabled = false;
            }
            else
            {
                keyPrevPageButton.IsEnabled = true;
            }
            if (!paginationKey.isNext())
            {
                keyNextPageButton.IsEnabled = false;
            }
            else
            {
                keyNextPageButton.IsEnabled = true;
            }

            if (!paginationNote.isPrev())
            {
                notePrevPageButton.IsEnabled = false;
            }
            else
            {
                notePrevPageButton.IsEnabled = true;
            }
            if (!paginationNote.isNext())
            {
                noteNextPageButton.IsEnabled = false;
            }
            else
            {
                noteNextPageButton.IsEnabled = true;
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void keyCurPageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (keyCurPageTextBox.Text != string.Empty)
            {
                int newPage = Convert.ToInt32(keyCurPageTextBox.Text);
                if (newPage < 1 || newPage > paginationKey.maxPage)
                {
                    keyCurPageTextBox.Text = paginationKey.currentPage.ToString();
                }
                else
                {
                    paginationKey.currentPage = newPage;
                    RefreshTable();
                }
            }
            else
            {
                keyCurPageTextBox.Text = paginationKey.currentPage.ToString();
            }
        }

        private void noteCurPageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (noteCurPageTextBox.Text != string.Empty)
            {
                int newPage = Convert.ToInt32(noteCurPageTextBox.Text);
                if (newPage < 1 || newPage > paginationNote.maxPage)
                {
                    noteCurPageTextBox.Text = paginationNote.currentPage.ToString();
                }
                else
                {
                    paginationNote.currentPage = newPage;
                    RefreshTable();
                }
            }
            else
            {
                noteCurPageTextBox.Text = paginationNote.currentPage.ToString();
            }
        }

        private void keyPrevPageButton_Click(object sender, RoutedEventArgs e)
        {
            paginationKey.PrevPage();
            RefreshTable();
        }

        private void keyNextPageButton_Click(object sender, RoutedEventArgs e)
        {
            paginationKey.NextPage();
            RefreshTable();
        }

        private void notePrevPageButton_Click(object sender, RoutedEventArgs e)
        {
            paginationNote.PrevPage();
            RefreshTable();
        }

        private void noteNextPageButton_Click(object sender, RoutedEventArgs e)
        {
            paginationNote.NextPage();
            RefreshTable();
        }

        private void KeyTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (KeyTextBox.Text == "Поиск ключа...")
            {
                KeyTextBox.Text = string.Empty;
                KeyTextBox.Foreground = System.Windows.Media.Brushes.Black;
                KeyTextBox.FontStyle = FontStyles.Normal;
            }
        }

        private void KeyTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(KeyTextBox.Text))
            {
                KeyTextBox.Text = "Поиск ключа...";
                KeyTextBox.Foreground = Brushes.Gray;
                KeyTextBox.FontStyle = FontStyles.Italic;
            }
        }

        private void NoteTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NoteTextBox.Text == "Поиск заметки...")
            {
                NoteTextBox.Text = string.Empty;
                NoteTextBox.Foreground = System.Windows.Media.Brushes.Black;
                NoteTextBox.FontStyle = FontStyles.Normal;
            }
        }

        private void NoteTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NoteTextBox.Text))
            {
                NoteTextBox.Text = "Поиск заметки...";
                NoteTextBox.Foreground = Brushes.Gray;
                NoteTextBox.FontStyle = FontStyles.Italic;
            }
        }
    }
}
