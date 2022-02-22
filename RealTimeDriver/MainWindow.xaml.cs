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
        private object locker = new object();
        public MainWindow()
        {
            InitializeComponent();
            this.AddressCombo.ItemsSource = new List<string> { "ADDR013", "ADDR014", "ADDR015", "ADDR016" };
            Value.Text = "0";
            this.DataContext = this;
        }

        private void StartClick(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                lock(locker)
                {
                    Thread.Sleep(3000);
                    double Value1 = GetRandomNumber(Convert.ToDouble(LowLimit.Text), Convert.ToDouble(HighLimit.Text));
                    Value.Text = Convert.ToString(Value1);
                }
            }
        }
        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
