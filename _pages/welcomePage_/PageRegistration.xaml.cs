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

namespace Collage.src._pages.welcomePage_
{
    /// <summary>
    /// Логика взаимодействия для PageRegistration.xaml
    /// </summary>
    public partial class PageRegistration : Page
    {
        private Entities entities = new Entities();
        private int disciplineID;
        LoggingSystem loggingSystem = new LoggingSystem();
        public PageRegistration()
        {
            InitializeComponent();

            var queryDiscipline = from Discipline in entities.Discipline
                                  select Discipline.named;

            cbItemDiscipline.ItemsSource = queryDiscipline.ToList();
           
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
            try
            {
            // Форматируем номер телефона
            phoneNumber = string.Format("+7 ({0}) {1}-{2}-{3}", phoneNumber.Substring(1, 3), phoneNumber.Substring(4, 3), phoneNumber.Substring(7, Math.Min(phoneNumber.Length - 7, 2)), phoneNumber.Substring(9, Math.Min(phoneNumber.Length - 9, 2)));
            return phoneNumber;
            }
            catch
            {

            }
            return phoneNumber;

        }

        private void cbItemDiscipline_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Переборка выбранного элемента в комбобоксе
            string discipline = cbItemDiscipline.SelectedValue.ToString();

            foreach (var type in entities.Discipline)
            {
                if (type.named == discipline)
                    disciplineID = Convert.ToInt32(type.id_discipline);
            }
        }

        private void btRegister_Click(object sender, RoutedEventArgs e)
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

            // Проверка на то что пользователь не выбрал предмет
            if (cbItemDiscipline.SelectedItem == null)
                error++;
            // Проверка на пыстые поля PasswordBox
            if (tbPassword.Password.Trim() == null || tbConfirmPassword.Password.Trim() == null)
                error++;
            // Проверка на совпадение пароля
            if (tbPassword.Password != tbConfirmPassword.Password)
            {
                MessageBox.Show("Пароли не сопадают!");
                loggingSystem.LogginSystem("Регистрация, пароля не совпадают");
                return;
            }
            // Если есть пустые поля вывод сообщения
            if (error >= 1)
            {
                MessageBox.Show("Не все поля заполнены и не все элементы выбраны!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = entities.Users.FirstOrDefault(u => u.login == tbLogin.Text.Trim());

            if (user != null)
            {
                MessageBox.Show("Такой логин уже существует!", "Уведомление!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Users user_ = new Users();
            Line_User_Discipline line_User_Discipline = new Line_User_Discipline();
            var _user = entities.Users.ToList();
            try
            {
                user_.login = tbLogin.Text.Trim();
                user_.password = tbPassword.Password.Trim();
                user_.fio = tbFio.Text.Trim();
                user_.phone_number = tbPhone.Text;
                user_.nickname = tbPhone.Text;
                user_.id_type_user = 1;


                entities.Users.Add(user_);
                entities.SaveChanges();

                line_User_Discipline.id_user = user_.id_user;
                line_User_Discipline.id_discipline = disciplineID;

                for (int i = 0; i < _user.Count; i++)
                {
                    Chatings chatings = new Chatings();
                    chatings.id_user_one = user_.id_user;
                    chatings.id_user_two = _user[i].id_user;
                    entities.Chatings.Add(chatings);
                }

                entities.SaveChanges();


                Application.Current.Properties["UserID"] = user_.id_user;
                Application.Current.Properties["NickName"] = user.nickname;

                MessageBox.Show("Успешная регистрация!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                loggingSystem.LogginSystem("Успешная авторизация");
                NavigationService.Navigate(new PageSingIn());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void hlLogin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageSingIn());
        }
    }
}
