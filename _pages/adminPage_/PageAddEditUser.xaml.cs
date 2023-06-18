using Collage.src.DateBase;
using Collage.src.scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Collage.src._pages.adminPage_
{
    /// <summary>
    /// Логика взаимодействия для PageAddEditUser.xaml
    /// </summary>
    public partial class PageAddEditUser : Page
    {
        Entities entities = new Entities();
        private string statusEdit = "Edit";
        private int itemid = Convert.ToInt32(Application.Current.Properties["AddEditItemId"]);
        private Users _item = new Users();
        LoggingSystem loggingSystem = new LoggingSystem();
        public PageAddEditUser()
        {
            InitializeComponent();

            var item = entities.Users.FirstOrDefault(i => i.id_user == itemid);

            if (item != null)
            {
                statusEdit = "Edit";

                DataContext = item;
            }
            else
            {
                statusEdit = "Add";
                DataContext = _item;
            }
            cbItem.ItemsSource = entities.User_types.ToList();
        }

        private void tbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Тут крч магия происходит, он из говна и палоке делает нормальный формат номера телефона под русский стиль
            e.Handled = !IsTextAllowed(e.Text);
            if (!e.Handled && e.Text != "\b")
            {
                var textBox = sender as TextBox;
                var text = textBox.Text.Insert(textBox.CaretIndex, e.Text);
                var formattedText = FormatPhoneNumber(text);
                if (formattedText != text)
                {
                    textBox.Text = formattedText;
                    textBox.CaretIndex = formattedText.Length;
                    e.Handled = true;
                }
            }
        }

        private bool IsTextAllowed(string text)
        {
            // Хз что это, без этого не пашет /(-_-)/\/ Это ChatGPT подсказал
            return Regex.IsMatch(text, @"^\d$");
        }

        private string FormatPhoneNumber(string phoneNumber)
        {
            // Проверяем длину номера телефона
            if (phoneNumber.Length < 10)
            {
                return phoneNumber;
            }

            // Убираем все символы, кроме цифр
            phoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

            // Форматируем номер телефона
            phoneNumber = string.Format("+7 ({0}) {1}-{2}-{3}", phoneNumber.Substring(1, 3), phoneNumber.Substring(4, 3), phoneNumber.Substring(7, Math.Min(phoneNumber.Length - 7, 2)), phoneNumber.Substring(9, Math.Min(phoneNumber.Length - 9, 2)));

            return phoneNumber;
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            int error = 0;
            // Цикл по проверке всех TextBox на пустые значения
            foreach (TextBox textBox in mainStackPanel.Children.OfType<TextBox>())
            {
                if (string.IsNullOrEmpty(textBox.Text.Trim()))
                {
                    error++;
                }
            }
            // Если есть пустые поля вывод сообщения
            if (error >= 1)
            {
                MessageBox.Show("Не все поля заполнены и не все элементы выбраны!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (statusEdit == "Edit")
            {
                var item = entities.Users.FirstOrDefault(i => i.id_user == itemid);
                try
                {
                    entities.SaveChanges();

                    MessageBox.Show("Успех");
                    loggingSystem.LogginAdminUse($"Обновление пользователя {item.fio}");

                    NavigationService.Navigate(new PageUserPanel());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (statusEdit == "Add")
            {
                try
                {
                    entities.Users.Add(_item);
                    entities.SaveChanges();
                    // Выводим сообщение об успехе
                    MessageBox.Show("Пользователь успешно добавлен в базу данных!");

                    loggingSystem.LogginAdminUse($"Добавление пользователя {_item.fio}");
                    NavigationService.Navigate(new PageUserPanel());
                }
                catch (Exception ex)
                {
                    // Выводим сообщение об ошибке, если что-то пошло не так
                    MessageBox.Show("Произошла ошибка при добавлении клиента в базу данных: " + ex.Message);
                }
            }
        }

        private void hlExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageUserPanel());
        }
    }
}
