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
    /// Interaction logic for AddAlarm.xaml
    /// </summary>
    public partial class AddAlarm : Window
    {
        public AnalogInput SelectedAI { get; set; }
        public AddAlarm(AnalogInput SelectedAI)
        {
            InitializeComponent();
            this.Type.ItemsSource = new List<string> { "High", "Low" };
            this.Priority.ItemsSource = new List<string> { "1", "2", "3" };
            this.SelectedAI = SelectedAI;
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void AlarmAdd(object sender, RoutedEventArgs e)
        {
            if(ValidateInput() == true)
            {
                double treshold;
                Priorities helpEnum;
                Types typeHelp;
                if (Double.TryParse(Treshold.Text, out treshold))
                {
                    string type = Type.Text;
                    string priority = Priority.Text;
                    string message = Message.Text;
                    if (priority == "1")
                    {
                        helpEnum = Priorities.ONE;
                    }else if(priority == "2")
                    {
                        helpEnum = Priorities.TWO;
                    }else
                    {
                        helpEnum = Priorities.THREE;
                    }

                    if (type == "High")
                    {
                        typeHelp = Types.HIGH;
                    }else
                    {
                        typeHelp = Types.LOW;
                    }
                    Alarm alarm = new Alarm {TagName = SelectedAI.TagName, Treshold = treshold, Types = typeHelp, Priorities = helpEnum, State = State.OUT, Message = message };
                    MainWindow.proxy2.AddAlarmToAI(alarm, SelectedAI);
                    this.Close();
                }
                else
                {
                    Treshold.BorderBrush = Brushes.Red;
                    MessageBox.Show("Treshold must be a number!");
                }
            }
            else
            {
                MessageBox.Show("Inputs are not valid!");
            }
        }
        private bool ValidateInput()
        {
            if (Treshold.Text.Length == 0 || Type.Text.Length == 0 || Priority.Text.Length == 0  || Message.Text.Length == 0 ||  Treshold.Text.Trim().Equals("") || Message.Text.Trim().Equals(""))
            {
                if (Treshold.Text.Length == 0)
                {
                    Treshold.BorderBrush = Brushes.Red;
                }
                if (Priority.Text.Length == 0)
                {
                    Priority.BorderBrush = Brushes.Red;
                }
                if (Type.Text.Length == 0)
                {
                    Type.BorderBrush = Brushes.Red;
                }
                if(Message.Text.Length == 0)
                {
                    Message.BorderBrush = Brushes.Red;
                }
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
