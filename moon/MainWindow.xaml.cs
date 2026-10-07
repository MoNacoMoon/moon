using System;
using System.Linq;
using System.Windows;
using moon.Models;

namespace moon
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using var context = new UserAuthDbContext();
                var listQuestions = context.СекретныйВопрос
                    .Select(i => i.СекретныйВопрос1)
                    .ToList();

                qwest.ItemsSource = listQuestions;
                if (listQuestions.Count > 2)
                {
                    qwest.SelectedIndex = 2;
                }
                else if (listQuestions.Count > 0)
                {
                    qwest.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}");
            }
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (chkAgree.IsChecked != true)
                {
                    MessageBox.Show("Необходимо согласиться с условиями");
                    return;
                }

                if (string.IsNullOrWhiteSpace(lName.Text) || string.IsNullOrWhiteSpace(name.Text))
                {
                    MessageBox.Show("Заполните Фамилию и Имя");
                    return;
                }

                if (passw.Password.Length < 6)
                {
                    MessageBox.Show("Минимальная длина пароля 6 символов");
                    return;
                }

                if (passw.Password != passwRepeat.Password)
                {
                    MessageBox.Show("Пароли не совпадают");
                    return;
                }

                using var context = new UserAuthDbContext();
                string selectedQuestion = qwest.Text;

                int questionId = context.СекретныйВопрос
                    .Where(i => i.СекретныйВопрос1 == selectedQuestion)
                    .Select(i => i.КодСекретногоВопроса)
                    .FirstOrDefault();

                if (questionId == 0)
                {
                    questionId = 1;
                }

                var newUser = new Пользователь
                {
                    Фамилия = lName.Text.Trim(),
                    Имя = name.Text.Trim(),
                    ЭлектроннаяПочта = eMail.Text.Trim(),
                    Пароль = passw.Password,
                    КодовоеСлово = word.Text.Trim(),
                    ОтветНаСекретныйВопрос = otvet.Text.Trim(),
                    КодСекретногоВопроса = questionId
                };

                context.Пользователь.Add(newUser);
                context.SaveChanges();

                MessageBox.Show($"Пользователь {lName.Text} добавлен");

                var userWin = new UserWin();
                userWin.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}");
            }
        }

        private void btnOpenUserWin_Click(object sender, RoutedEventArgs e)
        {
            var userWin = new UserWin();
            userWin.Show();
        }

        private void btnOpenDebug_Click(object sender, RoutedEventArgs e)
        {
            var debugWin = new DebugDemoWin();
            debugWin.Show();
        }
    }
}