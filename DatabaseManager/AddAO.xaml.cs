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

            foreach(var d in SCADA.adress.Keys)
            {
                if (SCADA.adress[d]==false)
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
            SCADA.adress[combo] = true;
            double initialValue = Convert.ToDouble(Valuebox.Text);
            double low = Convert.ToDouble(LowLimit.Text);
            double high = Convert.ToDouble(HighLimit.Text);
            AnalogOutput analogOutput = new AnalogOutput { TagName=tag, Description=desc, HighLimit=high, LowLimit=low, InitialValue=initialValue, IOAdress=combo };
            MainWindow.proxy2.AddAO(analogOutput);

            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
