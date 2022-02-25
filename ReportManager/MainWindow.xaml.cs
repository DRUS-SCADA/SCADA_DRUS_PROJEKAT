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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReportManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ReportManagerClient proxy = new ReportManagerClient();
        public MainWindow()
        {
            InitializeComponent();
            
            this.Report1.ItemsSource = new List<string> { "1 hour", "1 day", "1 week", "1 month" };
            this.Report2.ItemsSource = new List<string> { "1", "2", "3"};
            this.Report3tag.ItemsSource = new List<string> { "Analog input", "Digital input" };
            this.Report3tagValues.ItemsSource = new List<string> { "1 hour", "1 day", "1 week", "1 month" };
            this.Report4tag.ItemsSource = new List<string> { "Analog input", "Digital input" };
        }
        private void ReportClick(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Tag;
            if (tag.ToString() == "1")
            {
                if (ValidateInputReport1())
                {
                    Reports reports = new Reports("1");
                    reports.Show();
                    this.Hide();
                    DeleteBoxes();
                }
                else
                {
                    MessageBox.Show("Inputs are not valid!");
                    Report1.ClearValue(Border.BorderBrushProperty);
                }
            }
            else if (tag.ToString() == "2")
            {
                if (ValidateInputReport2())
                {
                    Reports reports = new Reports("2");
                    reports.Show();
                    this.Hide();
                    DeleteBoxes();
                }
                else
                {
                    MessageBox.Show("Inputs are not valid!");
                    Report2.ClearValue(Border.BorderBrushProperty);
                }
            }
            else if (tag.ToString() == "3")
            {
                if (ValidateInputReport3())
                {
                    Reports reports = new Reports("3");
                    reports.Show();
                    this.Hide();
                    DeleteBoxes();
                }
                else
                {
                    MessageBox.Show("Inputs are not valid!");
                    Report3tagValues.ClearValue(Border.BorderBrushProperty);
                    Report3tag.ClearValue(Border.BorderBrushProperty);
                }
            }
            else if (tag.ToString() == "4")
            {
                if (ValidateInputReport4())
                {
                    if (Report4tag.Text == "Analog input")
                    {
                        if (proxy.IsFoundInputAI(Report4tagName.Text))
                        {
                            Reports reports = new Reports("4");
                            reports.Show();
                            this.Hide();
                            DeleteBoxes();
                        }
                        else
                        {
                            MessageBox.Show("Input with that tag name doesn't exist!");
                            
                        }

                    }
                    else if (Report4tag.Text == "Digital input")
                    {

                        if (proxy.IsFoundInputDI(Report4tagName.Text))
                        {
                            Reports reports = new Reports("4");
                            reports.Show();
                            this.Hide();
                            DeleteBoxes();
                        }
                        else
                        {
                            MessageBox.Show("Input with that tag name doesn't exist!");
                            
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Inputs are not valid!");
                    Report4tagName.ClearValue(Border.BorderBrushProperty);
                    Report4tag.ClearValue(Border.BorderBrushProperty);
                }
            }
            else if (tag.ToString() == "5")
            {
                Reports reports = new Reports("5");
                reports.Show();
                this.Hide();
            }
            else if (tag.ToString() == "6")
            {
                Reports reports = new Reports("6");
                reports.Show();
                this.Hide();
            }
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private bool ValidateInputReport1()
        {
            if(Report1.Text.Length == 0)
            {
                Report1.BorderBrush = Brushes.Red;
                return false;
            }else
            {
                return true;
            }
        }
        private bool ValidateInputReport2()
        {
            if (Report2.Text.Length == 0)
            {
                Report2.BorderBrush = Brushes.Red;
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool ValidateInputReport3()
        {
            if (Report3tag.Text.Length == 0 || Report3tagValues.Text.Length == 0)
            {
                if(Report3tag.Text.Length == 0)
                {
                    Report3tag.BorderBrush = Brushes.Red;
                }
                if(Report3tagValues.Text.Length == 0)
                {
                    Report3tagValues.BorderBrush = Brushes.Red;
                }
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool ValidateInputReport4()
        {
            if (Report4tag.Text.Length == 0 || Report4tagName.Text.Length == 0 || Report4tagName.Text.Trim().Equals(""))
            {
                if (Report4tag.Text.Length == 0)
                {
                    Report4tag.BorderBrush = Brushes.Red;
                }
                if (Report4tagName.Text.Length == 0)
                {
                    Report4tagName.BorderBrush = Brushes.Red;
                }
                return false;
            }
            else
            {
                return true;
            }
        }
        private void DeleteBoxes()
        {
            Report1.Text = string.Empty;
            Report2.Text = string.Empty;
            Report3tag.Text = string.Empty;
            Report3tagValues.Text = string.Empty;
            Report4tag.Text = string.Empty;
            Report4tagName.Text = string.Empty;
        }
    }
}
