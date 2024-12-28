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
    /// Логика взаимодействия для EditKeyWindow.xaml
    /// </summary>
    public partial class EditKeyWindow : Window
    {
        int oldKeyID;
        KeyManager _keyManager;
        public EditKeyWindow(int oldID, KeyManager keyManager)
        {
            InitializeComponent();
            oldKeyID = oldID;
            _keyManager = keyManager;
            keyTextBox.Text = _keyManager.Get(oldID).Title;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            Database.Model.Key newKey = new Database.Model.Key();
            newKey.Title = keyTextBox.Text;
            _keyManager.Edit(oldKeyID, newKey);
            Close();
        }
    }
}
