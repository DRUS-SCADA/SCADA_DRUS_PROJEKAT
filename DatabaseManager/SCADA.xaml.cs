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
using DatabaseManager.ServiceReference2;
using DatabaseManager.ServiceReference1;

namespace DatabaseManager
{
    /// <summary>
    /// Interaction logic for SCADA.xaml
    /// </summary>
    public partial class SCADA : Window
    {
        //DigitalOutput SelectedDO { get; set; }
        
        MainWindow mw = (MainWindow)Application.Current.MainWindow;
        string token1;
        
        public SCADA(string token)
        {
            InitializeComponent();
            token1 = token;
            

            this.DataContext = this;
            
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Tab1.SelectedIndex == 0)
            {
                AddDI addDI = new AddDI();
                addDI.ShowDialog();
                
            }
            else if (Tab1.SelectedIndex == 1)
            {
                AddDO addDO = new AddDO();
                addDO.ShowDialog();
                
            }
            else if (Tab1.SelectedIndex == 2)
            {
                AddAI addAI = new AddAI();
                addAI.ShowDialog();
                
            }
            else 
            {
                AddAO addAO = new AddAO();
                addAO.ShowDialog();
                
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            mw.Show();
        }

        public void Logout_Click(object sender, RoutedEventArgs e)
        {
            bool logout = MainWindow.proxy.Logout(token1);
            if (logout == true)
            {
                mw.TextBox_GotFocus2(sender,e);
                this.Close();
                mw.Show();
            }
            else
            {
                MessageBox.Show("You cant logout");
            }
        }

        private void ProfileDelete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("Are you sure you want to delete this profile? ", "Delete", MessageBoxButton.YesNo);
            if (mbr == MessageBoxResult.Yes)
            {
                string username = mw.GetUsername;
                string password = mw.GetPassword;

                bool delete = MainWindow.proxy.DeleteProfile(username, password);
                if (delete == true)
                {
                    MessageBox.Show("Profile succesfully deleted!");
                    this.Close();
                    mw.Show();
                }
                else
                {
                    MessageBox.Show("Something went wrong, try again");
                }
            }
          
        }

        private void PasswordChange(object sender, RoutedEventArgs e)
        {
            ChangePassword changePass = new ChangePassword();
            changePass.Show();
        }
    }
}
