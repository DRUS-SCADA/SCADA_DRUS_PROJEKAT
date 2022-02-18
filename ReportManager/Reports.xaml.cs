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
        List<Alarm> alarms = new List<Alarm>();
        public Reports(string buttonNumber)
        {
            InitializeComponent();
            this.DataContext = this;
            if(buttonNumber == "1")
            {
                WriteReport1();
            }
            else if (buttonNumber == "2")
            {
                WriteReport2();
            }
            else if(buttonNumber == "3")
            {
                WriteReport3();
            }else if(buttonNumber == "4")
            {
                WriteReport4();
            }
        }
        private void WriteReport1()
        {
            double time;
            if (mw.Report1.Text == "1 hour")
            {
                time = -60;
            }
            else if (mw.Report1.Text == "1 day")
            {
                time = -1440;
            }
            else if (mw.Report1.Text == "1 week")
            {
                time = -10080;
            }
            else
            {
                time = -44640;
            }
            alarms = MainWindow.proxy.Report1(time).ToList();
            foreach (var i in alarms)
            {
                ReportsList.Text += i.TagName + " " + i.Priorities + " " + i.Treshold + " " + i.Types + " " + i.DateTime + "\n";
            }
        }
        private void WriteReport2()
        {
            string priority;
            if (mw.Report2.Text == "1")
            {
                priority = "ONE";
            }
            else if (mw.Report2.Text == "2")
            {
                priority = "TWO";
            }
            else
            {
                priority = "THREE";
            }
            alarms = MainWindow.proxy.Report2(priority).ToList();
            foreach (var i in alarms)
            {
                ReportsList.Text += i.TagName + " " + i.Priorities + " " + i.Treshold + " " + i.Types + " " + i.DateTime + "\n";
            }
        }
        private void WriteReport3()
        {
            double time;
            if (mw.Report3tagValues.Text == "1 hour")
            {
                time = -60;
            }
            else if (mw.Report3tagValues.Text == "1 day")
            {
                time = -1440;
            }
            else if (mw.Report3tagValues.Text == "1 week")
            {
                time = -10080;
            }
            else
            {
                time = -44640;
            }
            Tag tag1 = new Tag();
            tag1 = MainWindow.proxy.Report3(mw.Report3tag.Text, time);
            if(mw.Report3tag.Text == "Analog input")
                foreach(var i in tag1.analogInputs)
                {
                    ReportsList.Text += i.TagName + " " + i.TimeStamp + " " + i.Value + "\n";
                }
            else if (mw.Report3tag.Text == "Digital input")
            {
                foreach (var i in tag1.digitalInputs)
                {
                    ReportsList.Text += i.TagName + " " + i.TimeStamp + " " + i.Value + "\n";
                }
            }
        }
        private void WriteReport4()
        {
            Tag tag1 = new Tag();
            tag1 = MainWindow.proxy.Report6(mw.Report4tagName.Text, mw.Report4tag.Text);

            if (mw.Report4tag.Text == "Analog input")
            {
                foreach (var i in tag1.analogInputs)
                {
                    ReportsList.Text += i.TagName + " " + i.TimeStamp + " " + i.Value + "\n";
                }
            }
            else if (mw.Report4tag.Text == "Digital input")
            {
                foreach (var i in tag1.digitalInputs)
                {
                    ReportsList.Text += i.TagName + " " + i.TimeStamp + " " + i.Value + "\n";
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
