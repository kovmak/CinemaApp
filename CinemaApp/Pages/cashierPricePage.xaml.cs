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

namespace CinemaApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для cashierPricePage.xaml
    /// </summary>
    public partial class cashierPricePage : Page
    {
        public cashierPricePage()
        {
            InitializeComponent();
        }

        private void payButton_Click(object sender, RoutedEventArgs e)
        {
            fullTicketPrice.Clear();
            studentTicketPrice.Clear();
            panshioneerTicketPrice.Clear();
        }

        private void fullTicketPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            FullPriceCount();
        }

        private void studentTicketPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            FullPriceCount();
        }

        private void panshioneerTicketPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            FullPriceCount();
        }

        private void FullPriceCount()
        {
            int sum = 0;
            int sumStud = 0;
            int sumPansh = 0;

            // Отримайте число, введене в TextBox для повних квитків
            if (int.TryParse(fullTicketPrice.Text, out int targetNumber))
            {
                for (int i = 1; i <= targetNumber; i++)
                {
                    sum += 60;
                }
            }

            // Отримайте число, введене в TextBox для студентських квитків
            if (int.TryParse(studentTicketPrice.Text, out int targetNumberStud))
            {
                for (int i = 1; i <= targetNumberStud; i++)
                {
                    sumStud += 40;
                }
            }

            // Отримайте число, введене в TextBox для пенсійних квитків
            if (int.TryParse(panshioneerTicketPrice.Text, out int targetNumberPansh))
            {
                for (int i = 1; i <= targetNumberPansh; i++)
                {
                    sumPansh += 30;
                }
            }

            // Обчисліть загальну суму
            int totalSum = sum + sumStud + sumPansh;

            // Оновіть відповідний TextBox або інший UI-елемент зі значенням загальної суми
            fullPrice.Text = totalSum.ToString();
        }

    }
}
