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
    /// Логика взаимодействия для filmsPage.xaml
    /// </summary>
    public partial class filmsPage : Page
    {
        private int selectedLevel;

        public filmsPage(int selectedLevel)
        {
            InitializeComponent();
            this.selectedLevel = selectedLevel;
    }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new OrderPage(selectedLevel));
        }

    }
}
