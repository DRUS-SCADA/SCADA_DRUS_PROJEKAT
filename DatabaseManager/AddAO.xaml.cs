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
using System.Windows.Shapes;

namespace DatabaseManager
{
    /// <summary>
    /// Interaction logic for AddAO.xaml
    /// </summary>
    public partial class AddAO : Window
    {
        
        public List<string> IOAdress = new List<string>();

        public AddAO()
        {
            InitializeComponent();

            foreach(var d in SCADA.adressAO.Keys)
            {
                if (SCADA.adressAO[d]==false)
                {
                    IOAdress.Add(d);
                }
            }
            this.IoCombo.ItemsSource = IOAdress;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if(ValidateInput()==true)
            {
                double initialValue;
                double low;
                double high;
                if(Double.TryParse(Valuebox.Text, out initialValue) && Double.TryParse(HighLimit.Text, out high) && Double.TryParse(LowLimit.Text, out low))
                {
                    string tag = Idbox.Text;
                    string desc = Descriptionbox.Text;
                    string combo = IoCombo.Text;
                    SCADA.adressAO[combo] = true;
                    AnalogOutput analogOutput = new AnalogOutput { TagName = tag, Description = desc, HighLimit = high, LowLimit = low, InitialValue = initialValue, IOAdress = combo };
                    MainWindow.proxy2.AddAO(analogOutput);

                    this.Close();
                }else
                {
                    Valuebox.BorderBrush = Brushes.Red;
                    HighLimit.BorderBrush = Brushes.Red;
                    LowLimit.BorderBrush = Brushes.Red;
                    MessageBox.Show("Inputs must be a numbers!");
                }
            }else
            {
                MessageBox.Show("Inputs are not valid!");
            }
            
        }
        private bool ValidateInput()
        {
            if (Idbox.Text.Length == 0 || Descriptionbox.Text.Length == 0 || IoCombo.Text.Length == 0 || LowLimit.Text.Length == 0 || HighLimit.Text.Length == 0 ||  Idbox.Text.Trim().Equals("") || Descriptionbox.Text.Trim().Equals(""))
            {
                if (Idbox.Text.Length == 0)
                {
                    Idbox.BorderBrush = Brushes.Red;
                }
                if (Descriptionbox.Text.Length == 0)
                {
                    Descriptionbox.BorderBrush = Brushes.Red;
                }
                if (HighLimit.Text.Length == 0)
                {
                    HighLimit.BorderBrush = Brushes.Red;
                }
                if (LowLimit.Text.Length == 0)
                {
                    LowLimit.BorderBrush = Brushes.Red;
                }
                if(Valuebox.Text.Length == 0)
                {
                    Valuebox.BorderBrush = Brushes.Red;
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
