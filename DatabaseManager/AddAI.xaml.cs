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
    /// Interaction logic for AddAI.xaml
    /// </summary>
    public partial class AddAI : Window
    {
        public List<string> IOAdress = new List<string>();
        public AddAI()
        {
            InitializeComponent();
            foreach (var d in SCADA.adressAI.Keys)
            {
                if (SCADA.adressAI[d] == false)
                {
                    IOAdress.Add(d);
                }
            }
            this.IoCombo.ItemsSource = IOAdress;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string tag = Idbox.Text;
            string desc = Descriptionbox.Text;
            string combo = IoCombo.Text;
            double low = Convert.ToDouble(LowLimit.Text);
            double high = Convert.ToDouble(HighLimit.Text);
            string units = UnitsBox.Text;
            bool onoff = Convert.ToBoolean(ONOFF_scan.IsChecked);
            SCADA.adressAI[combo] = true;
            AnalogInput analogInput = new AnalogInput { TagName = tag, Description = desc, HighLimit = high, LowLimit = low,  IOAdress = combo, Units = units, ONOFF_scan = onoff};
            MainWindow.proxy2.AddAI(analogInput);
            
            this.Close();

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
