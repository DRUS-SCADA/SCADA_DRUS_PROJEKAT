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
            if (ValidateInput() == true)
            {
                double scanTime;
                double low;
                double high;
                if (Double.TryParse(ScanTime.Text, out scanTime) && Double.TryParse(HighLimit.Text, out high) && Double.TryParse(LowLimit.Text,out low))
                {
                    string tag = Idbox.Text;
                    string desc = Descriptionbox.Text;
                    string combo = IoCombo.Text;
                    string units = UnitsBox.Text;
                    bool onoff = Convert.ToBoolean(ONOFF_scan.IsChecked);
                    SCADA.adressAI[combo] = true;
                    AnalogInput analogInput = new AnalogInput { TagName = tag, Description = desc, HighLimit = high, LowLimit = low, IOAdress = combo, Units = units, ONOFF_scan = onoff, ScanTime = scanTime};
                    MainWindow.proxy2.AddAI(analogInput);

                    this.Close();
                }else
                {
                    HighLimit.BorderBrush = Brushes.Red;
                    LowLimit.BorderBrush = Brushes.Red;
                    ScanTime.BorderBrush = Brushes.Red;
                    MessageBox.Show("Inputs must be a number!");
                }
            }else
            {
                MessageBox.Show("Inputs are not valid!");
            }
            
        }
        private bool ValidateInput()
        {
            if (Idbox.Text.Length == 0 || Descriptionbox.Text.Length == 0 || IoCombo.Text.Length == 0 || LowLimit.Text.Length == 0 || HighLimit.Text.Length == 0 || UnitsBox.Text.Length == 0 || ScanTime.Text.Length == 0 || Idbox.Text.Trim().Equals("") || Descriptionbox.Text.Trim().Equals("") || UnitsBox.Text.Trim().Equals(""))
            {
                if (Idbox.Text.Length == 0)
                {
                    Idbox.BorderBrush = Brushes.Red;
                }
                if (Descriptionbox.Text.Length == 0)
                {
                    Descriptionbox.BorderBrush = Brushes.Red;
                }
                if (UnitsBox.Text.Length == 0)
                {
                    UnitsBox.BorderBrush = Brushes.Red;
                }
                if (HighLimit.Text.Length == 0)
                {
                    HighLimit.BorderBrush = Brushes.Red;
                }
                if (LowLimit.Text.Length == 0)
                {
                    LowLimit.BorderBrush = Brushes.Red;
                }
                if (ScanTime.Text.Length == 0)
                {
                    ScanTime.BorderBrush = Brushes.Red;
                }
                return false;
            }
            else
            {
                return true;
            }
        }
        
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
