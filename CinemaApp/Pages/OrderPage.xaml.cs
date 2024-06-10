using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using static CinemaApp.Pages.OrderPage;

namespace CinemaApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    
    public partial class OrderPage : Page
    {
        private List<Button> buttons = new List<Button>();

        public class Reservation
        {
            public int ButtonId { get; set; }
            public bool IsReserved { get; set; }
        }

        public class ReservationManager
        {
            private List<Reservation> reservations = new List<Reservation>();
            private string fileName = "reservations.json";

            public ReservationManager()
            {
                LoadReservations();
            }

            public void SaveReservations()
            {
                string json = JsonConvert.SerializeObject(reservations);
                File.WriteAllText(fileName, json);
            }

            public void LoadReservations()
            {
                if (File.Exists(fileName))
                {
                    string json = File.ReadAllText(fileName);
                    reservations = JsonConvert.DeserializeObject<List<Reservation>>(json);
                }
            }

            public void AddReservation(Reservation reservation)
            {
                reservations.Add(reservation);
                SaveReservations();
            }

            public List<Reservation> GetReservations()
            {
                return reservations;
            }

            public void RemoveReservation(int buttonId)
            {
                // Знайдіть індекс обраного елемента в списку
                int index = reservations.FindIndex(r => r.ButtonId == buttonId);

                if (index >= 0)
                {
                    // Видаліть обраний елемент
                    reservations.RemoveAt(index);
                    SaveReservations();
                }
            }

            public void ClearAllOrder()
            {
                reservations.Clear();
                 
                SaveReservations();
            }
        }


        private ReservationManager reservationManager = new ReservationManager();

        public OrderPage(int selectedLevel)
        {
            InitializeComponent();

            CancelButtonEnable(selectedLevel);
        }

        private void CreateButtons()
        {
            // Збереження стану бронювання між перемиканнями вкладок
            if (buttonReservations.Count == 0)
            {
                for (int i = 1; i <= 170; i++)
                {
                    buttonReservations[i] = false;
                }
            }

            for (int i = 1; i <= 170; i++)
            {
                var button = new Button
                {
                    Content = "Y",
                    Tag = i,
                    Width = 25,
                    Height = 25,
                    Margin = new Thickness(20, 20, 20, 0)
                };

                button.Click += ReserveButton;
                buttons.Add(button);
                Orderpanel.Children.Add(button);

                var cancelButton = new Button
                {
                    Content = "X",
                    Tag = i, // Використовуємо Tag для зберігання ID кнопки
                    Width = 25, // Ширина кнопки скасування
                    Height = 25, // Висота кнопки скасування
                    Margin = new Thickness(10, 20, 20, 0) // Відступи
                };

                cancelButton.Click += CancelReservation;
                buttons.Add(cancelButton);
                Orderpanel.Children.Add(cancelButton);
            }
        }

        private void ReserveButton(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                int buttonId = (int)button.Tag;

                if (!IsButtonReserved(buttonId))
                {
                    ReserveButton(buttonId);
                    button.IsEnabled = false;
                }
            }
        }

        private Dictionary<int, bool> buttonReservations = new Dictionary<int, bool>();

        private bool IsButtonReserved(int buttonId)
        {
            // Перевіряємо, чи кнопка з певним ID зарезервована.
            if (buttonReservations.ContainsKey(buttonId))
            {
                return buttonReservations[buttonId];
            }

            // Якщо кнопка з таким ID не знайдена, вона не зарезервована.
            return false;
        }

        private void ReserveButton(int buttonId)
        {
            if (!IsButtonReserved(buttonId))
            {
                buttonReservations[buttonId] = true;
                reservationManager.AddReservation(new Reservation { ButtonId = buttonId, IsReserved = true });
                reservationManager.SaveReservations();
            }
        }

        private void CancelReservation(object sender, RoutedEventArgs e)
        {
            if (sender is Button cancelButton)
            {
                int buttonId = (int)cancelButton.Tag;

                if (IsButtonReserved(buttonId))
                {
                    CancelButtonReservation(buttonId);

                    // Оновіть стан самої кнопки
                    foreach (Button button in buttons)
                    {
                        if ((int)button.Tag == buttonId)
                        {
                            button.IsEnabled = true;
                            break; // Знайшли відповідну кнопку, можна завершити пошук
                        }
                    }

                    reservationManager.SaveReservations();
                }
            }
        }
        private void CancelButtonReservation(int buttonId)
        {
            // Видаліть елемент зі словника, якщо він існує
            if (buttonReservations.ContainsKey(buttonId))
            {
                buttonReservations[buttonId] = false;
                reservationManager.RemoveReservation(buttonId);
            }


        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CreateButtons();
            reservationManager.LoadReservations();

            foreach (var reservation in reservationManager.GetReservations())
            {
                buttonReservations[reservation.ButtonId] = reservation.IsReserved;

                // Оновіть стан самої кнопки
                foreach (Button button in buttons)
                {
                    if ((int)button.Tag == reservation.ButtonId)
                    {
                        button.IsEnabled = !reservation.IsReserved;
                        break; // Знайшли відповідну кнопку, можна завершити пошук
                    }
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button button in buttons)
            {
                button.IsEnabled = true;
            }

            reservationManager.ClearAllOrder();
        }

        private void CancelButtonEnable(int selectedLevel)
        {
            if (selectedLevel == 0)
            {
                CancelButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                CancelButton.Visibility = Visibility.Visible;
            }
        }
    }
}
