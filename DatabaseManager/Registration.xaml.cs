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

namespace DatabaseManager
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }


        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            string name = FirstName.Text;
            string surname = LastName.Text;
            string username1 = username.Text;
            string password = Password.Password;
            string passwordRepeat = ConfirmPassword.Password;
            if (password == passwordRepeat)
            {
                MainWindow.proxy.Registration(name, surname, username1, password);
                this.Close();
            }else
            {
                MessageBox.Show("Passwords are not matched!");
            }
            
        }
    }
}
