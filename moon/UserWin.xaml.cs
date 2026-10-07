using System;
using System.Linq;
using System.Windows;
using moon.Models;

namespace moon
{
    public partial class UserWin : Window
    {
        public UserWin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                using var context = new UserAuthDbContext();
                dgUsers.ItemsSource = context.Пользователь.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
