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
    /// Interaction logic for ListOfAlarms.xaml
    /// </summary>
    public partial class ListOfAlarms : Window
    {
        public Alarm SelectedAlarm { get; set; }
        public AnalogInput SelectedAI { get; set; }
        public ListOfAlarms(AnalogInput SelectedAI)
        {
            InitializeComponent();
            this.SelectedAI = SelectedAI;
            dataAlarm.ItemsSource = MainWindow.proxy2.LoadDataToGridAlarm(SelectedAI);
            this.DataContext = this;
        }
        private void deleteBtnAlarm_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.proxy2.RemoveAlarm(SelectedAlarm, SelectedAI);
            dataAlarm.ItemsSource = MainWindow.proxy2.LoadDataToGridAlarm(SelectedAI);
        }
        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
