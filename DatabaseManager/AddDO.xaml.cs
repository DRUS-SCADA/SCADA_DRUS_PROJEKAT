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
        public DigitalOutput digitalOutput = new DigitalOutput();
        public AddDO()
        {
            InitializeComponent();
            this.DataContext = digitalOutput;
            this.IOcombo.ItemsSource = new List<string>() { "ADDR010" };
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
            DigitalOutput digitalOutput = new DigitalOutput { tag_name = tag, description = desc, IO_Adress = combo, initial_Value = initialValue };
            MainWindow.proxy2.AddDO(digitalOutput);

            this.Close();
            
        }
    }
}
