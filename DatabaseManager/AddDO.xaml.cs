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
    /// Interaction logic for AddDO.xaml
    /// </summary>
    public partial class AddDO : Window
    {
        public List<string> IOAdress = new List<string>();
        public List<string> initialValues = new List<string>() { "0", "1"};
        public AddDO()
        {
            InitializeComponent();

            foreach (var d in SCADA.adressDO.Keys)
            {
                if (SCADA.adressDO[d] == false)
                {
                    IOAdress.Add(d);
                }
            }
            this.IOcombo.ItemsSource = IOAdress;
            this.Valuebox.ItemsSource = initialValues;
        }
        

        private void Close_click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }

        private void AddDO_Click(object sender, RoutedEventArgs e)
        {
            if(ValidateInput() == true)
            {
                double initialValue = Convert.ToDouble(Valuebox.Text);
                string tag = Idbox.Text;
                string desc = Descriptionbox.Text;
                string combo = IOcombo.Text;
                SCADA.adressDO[combo] = true;
                DigitalOutput digitalOutput = new DigitalOutput { tag_name = tag, description = desc, IO_Adress = combo, initial_Value = initialValue };
                MainWindow.proxy2.AddDO(digitalOutput);

                this.Close();
            } else
            {
                MessageBox.Show("Inputs are not valid!");
            }
        }
        private bool ValidateInput()
        {
            if (Idbox.Text.Length == 0 || Descriptionbox.Text.Length == 0 || IOcombo.Text.Length == 0 || Valuebox.Text.Length == 0 || Idbox.Text.Trim().Equals("") || Descriptionbox.Text.Trim().Equals(""))
            {
                if (Idbox.Text.Length == 0)
                {
                    Idbox.BorderBrush = Brushes.Red;
                }
                if (Descriptionbox.Text.Length == 0)
                {
                    Descriptionbox.BorderBrush = Brushes.Red;
                }
                if (Valuebox.Text.Length == 0)
                {
                    Valuebox.BorderBrush = Brushes.Red;
                }
                if(IOcombo.Text.Length ==0)
                {
                    IOcombo.BorderBrush = Brushes.Red;
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
