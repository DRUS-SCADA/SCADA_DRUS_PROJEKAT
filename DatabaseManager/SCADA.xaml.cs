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
    /// Interaction logic for SCADA.xaml
    /// </summary>
    public partial class SCADA : Window
    {

        string token1;
        public SCADA(string token)
        {
            InitializeComponent();
            token1 = token;
            
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
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            bool logout = MainWindow.proxy.Logout(token1);
            if (logout == true)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("You cant logout");
            }
        }
    }
}
