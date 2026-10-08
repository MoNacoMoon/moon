using System;
using System.Diagnostics;
using System.Windows;

namespace moon
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }

    public partial class DebugDemoWin : Window
    {
        public DebugDemoWin()
        {
            InitializeComponent();
        }

        public void RunDemoAuto()
        {
            btnRunLoop_Click(this, new RoutedEventArgs());
            btnThrowCustom_Click(this, new RoutedEventArgs());
            btnThrowDivZero_Click(this, new RoutedEventArgs());
            btnDebugClasses_Click(this, new RoutedEventArgs());
        }

        private void Log(string message)
        {
            tbLog.AppendText($"{message}\n");
            tbLog.ScrollToEnd();
        }

        private void btnRunLoop_Click(object sender, RoutedEventArgs e)
        {
            Log("=== Вычисление ряда Лейбница для числа π ===");
            if (!int.TryParse(tbIterations.Text, out int n) || n <= 0)
            {
                n = 10;
            }

            double sum = 0.0;
            for (int i = 1; i <= n; i++)
            {
                // Точка останова F9
                double denominator = 2.0 * i - 1.0;
                double sign = (i % 2 == 1) ? 1.0 : -1.0;
                double term = sign / denominator;
                sum += term;

                Log($"Итерация {i}: term = {term:F6}, sum = {sum:F6}");
            }

            double pi = sum * 4.0;
            Log($"Результат π = {pi:F6} (Math.PI = {Math.PI:F6})\n");
        }

        private void btnThrowCustom_Click(object sender, RoutedEventArgs e)
        {
            Log("=== Проверка исключения ValidationException ===");
            try
            {
                ValidateData("", "123");
            }
            catch (ValidationException ex)
            {
                Log($"Перехвачено исключение: {ex.Message}");
            }
            finally
            {
                Log("Блок finally выполнен успешно.\n");
            }
        }

        private void ValidateData(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ValidationException("Логин не должен быть пустым");
            }
            if (password.Length < 6)
            {
                throw new ValidationException("Пароль менее 6 символов");
            }
        }

        private void btnThrowDivZero_Click(object sender, RoutedEventArgs e)
        {
            Log("=== Проверка исключения DivideByZeroException ===");
            try
            {
                int a = 10;
                int b = 0;
                int c = a / b;
                Log($"Результат: {c}");
            }
            catch (DivideByZeroException ex)
            {
                Log($"Ошибка: {ex.Message}");
            }
            finally
            {
                Log("Блок finally завершен.\n");
            }
        }

        private void btnDebugClasses_Click(object sender, RoutedEventArgs e)
        {
            Log("=== Тестирование отладочных классов (Debug / Trace / Stopwatch) ===");
            Debug.WriteLine("[DEBUG] Вызов метода btnDebugClasses_Click начат.");
            Trace.WriteLine("[TRACE] Фиксация контрольной точки в журнале трассировки.");

            int usersCount = 5;
            Debug.Assert(usersCount > 0, "Количество записей пользователей должно быть больше нуля");
            Log($"Debug.Assert: условие (usersCount > 0) истинно (проверено {usersCount} записей).");

            Stopwatch sw = Stopwatch.StartNew();
            double sum = 0.0;
            for (int i = 0; i < 50000; i++)
            {
                sum += Math.Sqrt(i);
            }
            sw.Stop();

            Debug.WriteLine($"[DEBUG] Вычисление контрольной суммы завершено за {sw.ElapsedMilliseconds} мс.");
            Log($"Stopwatch: время расчета 50 000 итераций = {sw.ElapsedMilliseconds} мс.");
            Log($"Debug.WriteLine и Trace.WriteLine отправили сообщения в окно отладки Visual Studio (Output).\n");
        }

        private void btnClearLog_Click(object sender, RoutedEventArgs e)
        {
            tbLog.Clear();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
