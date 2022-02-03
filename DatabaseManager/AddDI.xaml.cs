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
using DatabaseManager.ServiceReference1;

namespace DatabaseManager
{
    /// <summary>
    /// Interaction logic for AddDI.xaml
    /// </summary>
    public partial class AddDI : Window
    {
        public List<string> IOAdress1 = new List<string>();
        public AddDI()
        {
            InitializeComponent();
            foreach (var d in SCADA.adressDI.Keys)
            {
                if (SCADA.adressDI[d] == false)
                {
                    IOAdress1.Add(d);
                }
            }
            this.IOCombo.ItemsSource = IOAdress1;
        }

        private void AddDI_Click(object sender, RoutedEventArgs e)
        {
            string tag = Idbox.Text;
            string desc = Descriptionbox.Text;
            string combo = IOCombo.Text;
            bool onoff = Convert.ToBoolean(ONOFF_scan.IsChecked);
            SCADA.adressDI[combo] = true;
            DigitalInput digitalInput = new DigitalInput { TagName = tag, Description = desc, ONOFF_scan = onoff, IOAdress = combo};
            MainWindow.proxy2.AddDI(digitalInput);
            
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
