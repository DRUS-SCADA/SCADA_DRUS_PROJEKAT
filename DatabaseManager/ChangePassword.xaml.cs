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
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        string token1;
        SCADA scada1;
        MainWindow mw = (MainWindow)Application.Current.MainWindow;
        public ChangePassword(string token, SCADA scada)
        {
            InitializeComponent();
            scada1 = scada;
            token1 = token;
        }

        private void PasswordChangeClick(object sender, RoutedEventArgs e)
        {
            if (OldPassword.Password == mw.GetPassword)
            {
                string username = mw.GetUsername;
                string newPassword = NewPassword.Password;
                string indicate = MainWindow.proxy.ChangePassword(username, OldPassword.Password, newPassword);
                if (indicate == "Uspesna promena passworda")
                {
                    MessageBox.Show("Password succesfully changed!");
                    this.Close();
                    MainWindow.proxy.Logout(token1);
                    MainWindow.proxy2.clearData();
                    scada1.Close();
                    mw.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Password was not changed");
                }
            }else
            {
                MessageBox.Show("Old password is wrong!");
            }
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
