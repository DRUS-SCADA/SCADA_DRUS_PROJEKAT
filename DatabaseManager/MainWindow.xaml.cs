using DatabaseManager.ServiceReference1;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DatabaseManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static AuthenticationClient proxy = new AuthenticationClient();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = PasswordId.Password;
            string token = proxy.Login(username, password);

            if (token == "Neuspesno logovanje")
            {
                MessageBox.Show("Username or password are incorrect!");
            }else
            {
                SCADA scada = new SCADA();
                scada.ShowDialog();
            }
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.ShowDialog();

        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Username.Text = Username.Text == "Username" ? string.Empty : Username.Text;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Username.Text = Username.Text == string.Empty ? "Username" : Username.Text;
        }
        private void TextBox_GotFocus2(object sender, RoutedEventArgs e)
        {
            PasswordId.Password = "";
        }

        private void TextBox_LostFocus2(object sender, RoutedEventArgs e)
        {
            PasswordId.Password = "Password";
        }
        
    }
}
