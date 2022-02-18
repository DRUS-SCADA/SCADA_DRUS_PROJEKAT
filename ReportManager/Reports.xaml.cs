using ReportManager.ServiceReference1;
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

namespace ReportManager
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Window
    {
        MainWindow mw = (MainWindow)Application.Current.MainWindow;
        
        public Reports(string buttonNumber)
        {
            InitializeComponent();
            this.DataContext = this;
            if(buttonNumber == "1")
            {
                List<Alarm> alarms = new List<Alarm>();
                double time;
                if(mw.Report1.Text == "1 hour")
                {
                    time = -60;
                }else if(mw.Report1.Text == "1 day")
                {
                    time = -1440;
                }else if (mw.Report1.Text == "1 week")
                {
                    time = -10080;
                }else
                {
                    time = -44640;
                }
                alarms = MainWindow.proxy.Report1(time).ToList();
                foreach (var i in alarms)
                {
                    ReportsList.Text += i.TagName + " " + i.Priorities + " " + i.Treshold + " " + i.Types + " " + i.DateTime + "\n";
                }
            }
        }
        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ClearClick(object sender, RoutedEventArgs e)
        {
            this.ReportsList.Text = string.Empty;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mw.Show();
        }
    }
}
