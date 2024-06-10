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

namespace CinemaApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            string login = loginBox.Text;
            string password = PasswordBox.Password;
            string[] lines = System.IO.File.ReadAllLines(@"Data\Login.txt");
            bool isLoginCorrect = false;

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                if (parts.Length == 2)
                {
                    string fileLogin = parts[0];
                    string filePassword = parts[1];

                    if (login == fileLogin && password == filePassword)
                    {
                        isLoginCorrect = true;

                        int selectedLevel = 0;

                        Order newWindow = new Order(selectedLevel);
                        newWindow.Show();
                        this.Close();
                        break;
                    }
                }
            }

            if (!isLoginCorrect)
            {
                AdminLogin(login, password);
            }
        }
        private void AdminLogin(string login, string password)
        {
            string[] lines = System.IO.File.ReadAllLines(@"Data\adminlogin.txt");
            bool isLoginCorrect = false;

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                if (parts.Length == 2)
                {
                    string fileLogin = parts[0];
                    string filePassword = parts[1];

                    if (login == fileLogin && password == filePassword)
                    {
                        isLoginCorrect = true;

                        int selectedLevel = 1;

                        Order newWindow = new Order(selectedLevel);
                        newWindow.Show();
                        this.Close();
                        break;
                    }
                }
            }

            if (!isLoginCorrect)
            {
                MessageBox.Show("Error", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
