using RealTimeDriver.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public static string ContainerName { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            //proxy.makeDB();
            ContainerName = "KeyContainer";
            this.AddressCombo.ItemsSource = new List<string> { "ADDR013", "ADDR014", "ADDR015", "ADDR016" };
            this.DataContext = this;
        }

        private void StartClick(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                double limitHigh;
                double limitLow;
                if (Double.TryParse(HighLimit.Text, out limitHigh) && Double.TryParse(LowLimit.Text, out limitLow))
                {
                    string address = AddressCombo.Text;
                    t1 = new Thread(() => GeneratingSignals(limitHigh, limitLow, address));
                    t1.Start();
                    proxy.changeAddress(address);
                }
                else
                {
                    MessageBox.Show("Inputs must be number");
                }
            }
            else
            {
                MessageBox.Show("Inputs are not valid");
            }
           
        }
        private void GeneratingSignals(double high, double low, string address)
        {
            Random random = new Random();
            while (true)
            {
                Thread.Sleep(100);
                double Value1 = random.NextDouble() * (high - low) + low;
                byte[] signature = SignMessage(Value1, out byte[] hash);
                proxy.SendMessage(Value1, address, signature);
            }
        }
        private void CloseClick(object sender, RoutedEventArgs e)
        {
            t1.Abort();
            proxy.freeAddress(AddressCombo.Text);
            this.Close();
        }

        private bool ValidateInput()
        {
            if (HighLimit.Text.Length == 0 || LowLimit.Text.Length == 0 || AddressCombo.Text.Length == 0 )
            {
                if (HighLimit.Text.Length == 0)
                {
                    HighLimit.BorderBrush = Brushes.Red;
                }
                if (LowLimit.Text.Length == 0)
                {
                    LowLimit.BorderBrush = Brushes.Red;
                }
                if (AddressCombo.Text.Length == 0)
                {
                    AddressCombo.BorderBrush = Brushes.Red;
                }
               
                return false;
            }
            else
            {
                return true;
            }
        }
        private static byte[] SignMessage(double message, out byte[] hashValue)
        {
            using (SHA256 sha = SHA256Managed.Create())
            {
                string message1 = message.ToString();
                hashValue = sha.ComputeHash(Encoding.UTF8.GetBytes(message1));

                CspParameters csp = new CspParameters();
                csp.KeyContainerName = ContainerName;

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);
                var formatter = new RSAPKCS1SignatureFormatter(rsa);
                formatter.SetHashAlgorithm("SHA256");

                return formatter.CreateSignature(hashValue);
            }
        }
    }
}
