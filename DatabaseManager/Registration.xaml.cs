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

namespace DatabaseManager
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }


        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            string name = FirstName.Text;
            string surname = LastName.Text;
            string username1 = username.Text;
            string password = Password.Password;
            string passwordRepeat = ConfirmPassword.Password;
            if (ValidateInput() == true)
            {
                if (password == passwordRepeat)
                {
                    if(ValidateInput1() == true)
                    {
                        string validateusername = MainWindow.proxy.Registration(name, surname, username1, password);
                        if (validateusername == "Uspesno registrovanje")
                        {
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("User with this username already exists");
                        }
                    }else
                    {
                        MessageBox.Show("Password must have at least 6 characters!");
                    }
                }
                else
                {
                    MessageBox.Show("Passwords are not matched!");
                }
            }else
            {
                MessageBox.Show("Inputs are not valid!");
                FirstName.ClearValue(Border.BorderBrushProperty);
                LastName.ClearValue(Border.BorderBrushProperty);
                username.ClearValue(Border.BorderBrushProperty);
                Password.ClearValue(Border.BorderBrushProperty);
                ConfirmPassword.ClearValue(Border.BorderBrushProperty);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool ValidateInput()
        {
            if(FirstName.Text.Length == 0 || LastName.Text.Length==0 || username.Text.Length==0 || Password.Password.Length==0 || ConfirmPassword.Password.Length==0 || FirstName.Text.Trim().Equals("") || LastName.Text.Trim().Equals("") || username.Text.Trim().Equals("") || Password.Password.Trim().Equals("") || ConfirmPassword.Password.Trim().Equals(""))
            {
                if (FirstName.Text.Length == 0)
                {
                    FirstName.BorderBrush = Brushes.Red;
                }
                if (LastName.Text.Length == 0)
                {
                    LastName.BorderBrush = Brushes.Red;
                }
                if (username.Text.Length == 0)
                {
                    username.BorderBrush = Brushes.Red;
                }
                if (Password.Password.Length == 0)
                {
                    Password.BorderBrush= Brushes.Red;
                }
                if (ConfirmPassword.Password.Length == 0)
                {
                    ConfirmPassword.BorderBrush = Brushes.Red;
                }

                return false;
            }
            else
            {
                return true;
            }
        }
        private bool ValidateInput1()
        {
            if (Password.Password.Length < 6)
            {
                return false;
            }
            return true;
        }
    }
}
