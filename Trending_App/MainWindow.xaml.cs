using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
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
using Trending_App.ServiceReference1;

namespace Trending_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<AnalogInput> analogInputs = new ObservableCollection<AnalogInput>();
        public static ObservableCollection<DigitalInput> digitalInputs = new ObservableCollection<DigitalInput>();
        public MainWindow()
        {
            InitializeComponent();
            TrendingClient trendingProxy = new TrendingClient(new InstanceContext(new TrendingCallback()));
            trendingProxy.SubscriberInitialization();

            dataGrid.ItemsSource = digitalInputs;
            dataGrid2.ItemsSource = analogInputs;
            
            this.DataContext = this;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
