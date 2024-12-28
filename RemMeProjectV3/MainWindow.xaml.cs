using RemMeProjectV3.Database;
using RemMeProjectV3.Database.Model;
using RemMeProjectV3.Manager;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RemMeProjectV3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserManager userManager = new UserManager();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserNameTextBox.Text != string.Empty)
            {
                User? currentUser = userManager.GetByName(UserNameTextBox.Text);
                if (currentUser == null)
                {
                    User newUser = new User() { UserName = UserNameTextBox.Text };
                    userManager.Add(newUser);
                }
                int userID = userManager.GetByName(UserNameTextBox.Text).ID;
                NotebookWindow notebook = new NotebookWindow(userID);
                Hide();
                notebook.ShowDialog();
                Show();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите имя пользователя.",
                    "Предупреждение",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void UserNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                EnterButton_Click(sender, e);
            }
        }
    }
}