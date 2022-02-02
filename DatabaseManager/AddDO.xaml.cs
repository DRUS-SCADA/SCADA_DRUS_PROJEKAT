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
        }
        

        private void Close_click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }

        private void AddDO_Click(object sender, RoutedEventArgs e)
        {
            string tag = Idbox.Text;
            string desc = Descriptionbox.Text;
            string combo = IOcombo.Text;
            double initialValue = Convert.ToDouble(Valuebox.Text);
            SCADA.adressDO[combo] = true;
            DigitalOutput digitalOutput = new DigitalOutput { tag_name = tag, description = desc, IO_Adress = combo, initial_Value = initialValue };
            MainWindow.proxy2.AddDO(digitalOutput);

            this.Close();
            
        }
    }
}
