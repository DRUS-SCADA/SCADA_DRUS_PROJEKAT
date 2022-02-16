using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for SCADA.xaml
    /// </summary>
    public partial class SCADA : Window
    {
        public DigitalOutput SelectedDO { get; set; }
        public AnalogOutput SelectedAO { get; set; }
        public AnalogInput SelectedAI { get; set; }
        public DigitalInput SelectedDI { get; set; }

        public static Dictionary<string, bool> adressAO = new Dictionary<string, bool> { ["ADDR005"] = false, ["ADDR006"] = false, ["ADDR007"] = false, ["ADDR008"] = false };
        public static Dictionary<string, bool> adressAI = new Dictionary<string, bool> { ["ADDR001"] = false, ["ADDR002"] = false, ["ADDR003"] = false, ["ADDR004"] = false };
        public static Dictionary<string, bool> adressDO = new Dictionary<string, bool> { ["ADDR011"] = false, ["ADDR012"] = false};
        public static Dictionary<string, bool> adressDI = new Dictionary<string, bool> { ["ADDR009"] = false, ["ADDR010"] = false};

        MainWindow mw = (MainWindow)Application.Current.MainWindow;
        string token1;
        
        public SCADA(string token)
        {
            InitializeComponent();
            token1 = token;
            MainWindow.proxy2.ReadXML();
            MainWindow.proxy2.startPLC();
            dataGrid.ItemsSource = MainWindow.proxy2.LoadDataToGridDI();
            MainWindow.proxy2.LoadThreadDi();
            adressDI = MainWindow.proxy2.loadAdressDI(adressDI);
            dataGrid1.ItemsSource = MainWindow.proxy2.LoadDataToGrid();
            adressDO = MainWindow.proxy2.loadAdressDO(adressDO);
            dataGrid2.ItemsSource = MainWindow.proxy2.LoadDataToGridAI();
            MainWindow.proxy2.LoadThreadAi();
            adressAI = MainWindow.proxy2.loadAdressAI(adressAI);
            dataGrid3.ItemsSource = MainWindow.proxy2.LoadDataToGridAO();
            adressAO = MainWindow.proxy2.loadAdressAO(adressAO);

            this.DataContext = this;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Tab1.SelectedIndex == 0)
            {
                AddDI addDI = new AddDI();
                addDI.ShowDialog();
                dataGrid.ItemsSource = MainWindow.proxy2.LoadDataToGridDI();
            }
            else if (Tab1.SelectedIndex == 1)
            {
                AddDO addDO = new AddDO();
                addDO.ShowDialog();
                dataGrid1.ItemsSource = MainWindow.proxy2.LoadDataToGrid();
            }
            else if (Tab1.SelectedIndex == 2)
            {
                AddAI addAI = new AddAI();
                addAI.ShowDialog();
                dataGrid2.ItemsSource = MainWindow.proxy2.LoadDataToGridAI();
            }
            else 
            {
                AddAO addAO = new AddAO();
                addAO.ShowDialog();
                dataGrid3.ItemsSource = MainWindow.proxy2.LoadDataToGridAO();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void AlarmClick(object sender, RoutedEventArgs e)
        {
            if (Tab1.SelectedIndex == 2 && !dataGrid2.Items.IsEmpty)
            {
                AddAlarm addAlarm = new AddAlarm(SelectedAI);
                addAlarm.ShowDialog();
            }
            else if (Tab1.SelectedIndex != 2)
            {
                MessageBox.Show("You can add new alarm just for AI Tag!");
            }
            else
            {
                MessageBox.Show("You can add new alarm just after adding at least one AI Tag!");
            }
        }
        private void OpenAlarms(object sender, RoutedEventArgs e)
        {
            ListOfAlarms listOfAlarms = new ListOfAlarms(SelectedAI);
            listOfAlarms.ShowDialog();
        }

        public void Logout_Click(object sender, RoutedEventArgs e)
        {
            mw.TextBox_GotFocus2(sender, e);
            this.Close();
        }

        private void ProfileDelete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("Are you sure you want to delete this profile? ", "Delete", MessageBoxButton.YesNo);
            if (mbr == MessageBoxResult.Yes)
            {
                string username = mw.GetUsername;
                string password = mw.GetPassword;

                bool delete = MainWindow.proxy.DeleteProfile(username, password);
                if (delete == true)
                {
                    MessageBox.Show("Profile succesfully deleted!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Something went wrong, try again");
                }
            }
        }

        private void PasswordChange(object sender, RoutedEventArgs e)
        {
            ChangePassword changePass = new ChangePassword(token1, this);
            changePass.Show();
        }

        private void RemoveDO(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("Are you sure you want to delete this output? ", "Delete", MessageBoxButton.YesNo);

            if (mbr == MessageBoxResult.Yes)
            {
                MainWindow.proxy2.removeDO(SelectedDO);
                adressDO[SelectedDO.IO_Adress] = false;
                dataGrid1.ItemsSource = MainWindow.proxy2.LoadDataToGrid();
            }
        }
        private void RemoveAO(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("Are you sure you want to delete this output? ", "Delete", MessageBoxButton.YesNo);

            if (mbr == MessageBoxResult.Yes)
            {
                MainWindow.proxy2.removeAO(SelectedAO);
                adressAO[SelectedAO.IOAdress] = false;
                dataGrid3.ItemsSource = MainWindow.proxy2.LoadDataToGridAO();
            }
        }

        private void RemoveAI(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("Are you sure you want to delete this input? ", "Delete", MessageBoxButton.YesNo);

            if (mbr == MessageBoxResult.Yes)
            {
                MainWindow.proxy2.removeAI(SelectedAI);
                adressAI[SelectedAI.IOAdress] = false;
                dataGrid2.ItemsSource = MainWindow.proxy2.LoadDataToGridAI();
            }
        }

        private void RemoveDI(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("Are you sure you want to delete this input? ", "Delete", MessageBoxButton.YesNo);

            if (mbr == MessageBoxResult.Yes)
            {
                MainWindow.proxy2.removeDI(SelectedDI);
                adressDI[SelectedDI.IOAdress] = false;
                dataGrid.ItemsSource = MainWindow.proxy2.LoadDataToGridDI();
            }
        }
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            double change = SelectedAO.InitialValue;
            MainWindow.proxy2.SaveChanges(SelectedAO, change);
        }

        private void SaveClickAI(object sender, RoutedEventArgs e)
        {
            bool change = SelectedAI.ONOFF_scan;
            MainWindow.proxy2.SaveChangesAI(SelectedAI, change);
        }
        private void SaveClickDO(object sender, RoutedEventArgs e)
        {
            double change = SelectedDO.initial_Value;
            if (change == 0 || change == 1 )
            {
                MainWindow.proxy2.SaveChangesDO(SelectedDO, change);
            }else
            {
                MessageBox.Show("Digital value must be 0 or 1");
                SelectedDO.initial_Value = 0;
            }
        }
        private void SaveClickDI(object sender, RoutedEventArgs e)
        {
            bool change = SelectedDI.ONOFF_scan;
            MainWindow.proxy2.SaveChangesDI(SelectedDI, change);
        }
        private void WindowClosing(object sender, CancelEventArgs e)
        {
            MainWindow.proxy.Logout(token1);
            MessageBoxResult mbr2 = MessageBox.Show("Do you want to save data? ", "Save", MessageBoxButton.YesNo);
            if (mbr2 == MessageBoxResult.Yes)
            {
                MainWindow.proxy2.ClearDictionaries();
                MainWindow.proxy2.WriteXML();
                MainWindow.proxy2.clearData();
                MainWindow.proxy2.ClearCollections();
                mw.Show();
            }else
            {
                MainWindow.proxy2.clearData();
                adressAI = MainWindow.proxy2.loadAdressAIfree(adressAI);
                adressAO = MainWindow.proxy2.loadAdressAOfree(adressAO);
                adressDI = MainWindow.proxy2.loadAdressDIfree(adressDI);
                adressDO = MainWindow.proxy2.loadAdressDOfree(adressDO);
                MainWindow.proxy2.ClearCollections();
                MainWindow.proxy2.ClearDictionaries();
                MainWindow.proxy2.WriteXML();
                mw.Show();
            }
        }
    }
}
