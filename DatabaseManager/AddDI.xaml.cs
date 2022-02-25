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
            if (ValidateInput() == true)
            {
                double scanTime;
                if(Double.TryParse(Scanbox.Text,out scanTime))
                {
                    string tag = Idbox.Text;
                    string desc = Descriptionbox.Text;
                    string combo = IOCombo.Text;
                    bool onoff = Convert.ToBoolean(ONOFF_scan.IsChecked);

                    SCADA.adressDI[combo] = true;
                    DigitalInput digitalInput = new DigitalInput { TagName = tag, Description = desc, ONOFF_scan = onoff, IOAdress = combo, ScanTime = scanTime };
                    MainWindow.proxy2.AddDI(digitalInput);

                    this.Close();
                }else
                {
                    Scanbox.BorderBrush = Brushes.Red;
                    MessageBox.Show("Scan time must be a number!");
                    Scanbox.ClearValue(Border.BorderBrushProperty);

                }
            }else
            {
                MessageBox.Show("Inputs are not valid!");
                Idbox.ClearValue(Border.BorderBrushProperty);
                Descriptionbox.ClearValue(Border.BorderBrushProperty);
                IOCombo.ClearValue(Border.BorderBrushProperty);
                Scanbox.ClearValue(Border.BorderBrushProperty);


            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private bool ValidateInput()
        {
            if (Idbox.Text.Length == 0 || Descriptionbox.Text.Length == 0 || IOCombo.Text.Length == 0 || Scanbox.Text.Length == 0 || Idbox.Text.Trim().Equals("") || Descriptionbox.Text.Trim().Equals(""))
            {
                if (Idbox.Text.Length == 0)
                {
                    Idbox.BorderBrush = Brushes.Red;
                }
                if (Descriptionbox.Text.Length == 0)
                {
                    Descriptionbox.BorderBrush = Brushes.Red;
                }
                if (Scanbox.Text.Length == 0)
                {
                    Scanbox.BorderBrush = Brushes.Red;
                }
                if (IOCombo.Text.Length == 0)
                {
                    IOCombo.BorderBrush = Brushes.Red;
                }
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
