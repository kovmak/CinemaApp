using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using CinemaApp.Pages;

namespace CinemaApp
{
    /// <summary>
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Window
    {
        private int selectedLevel;

        public Order(int selectedLevel)
        {
            InitializeComponent();
            this.selectedLevel = selectedLevel;
            mainPage.Content = new filmsPage(selectedLevel);

            if (selectedLevel == 0)
            {
                priceButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                priceButton.Visibility = Visibility.Visible;
            }
        }

        private void orderButton_Click(object sender, RoutedEventArgs e)
        {

            mainPage.Content = new OrderPage(selectedLevel);
        }

        private void filmsButton_Click(object sender, RoutedEventArgs e)
        {
            mainPage.Content = new filmsPage(selectedLevel);
        }

        private void priceButton_Click(object sender, RoutedEventArgs e)
        {
            mainPage.Content = new cashierPricePage();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
