using RealTimeDriver.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RealTimeDriver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RealTimeDriverClient proxy = new RealTimeDriverClient();
        private Thread t1;
        public MainWindow()
        {
            InitializeComponent();
            this.AddressCombo.ItemsSource = new List<string> { "ADDR013", "ADDR014", "ADDR015", "ADDR016" };
            this.DataContext = this;
        }

        private void StartClick(object sender, RoutedEventArgs e)
        {
            double limitHigh = Convert.ToDouble(HighLimit.Text);
            double limitLow = Convert.ToDouble(LowLimit.Text);
            string address = AddressCombo.Text;

            t1 = new Thread(() => GeneratingSignals(limitHigh, limitLow,address));
            t1.Start();
        }
        private void GeneratingSignals(double high, double low, string address)
        {
            Random random = new Random();
            while (true)
            {
                Thread.Sleep(100);
                double Value1 = random.NextDouble() * (high - low) + low;
                proxy.SendMessage(Value1, address);
            }
        }
        private void CloseClick(object sender, RoutedEventArgs e)
        {
            t1.Abort();
            this.Close();
        }
    }
}
