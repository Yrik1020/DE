using Collage.src._pages.welcomePage_;
using Collage.src.DateBase;
using Collage.src.scripts;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Collage.src._pages.profilePage_
{
    /// <summary>
    /// Логика взаимодействия для PageProfilePanel.xaml
    /// </summary>
    public partial class PageProfilePanel : Page
    {
        private Entities entities = new Entities();
        private EditPassword editPassword = new EditPassword();
        private int UserID = Convert.ToInt32(Application.Current.Properties["UserID"]);
        private TextBox tbOldPassword = new TextBox();
        private TextBox tbNewPassword = new TextBox();
        LoggingSystem loggingSystem = new LoggingSystem();
        private byte[] fileBytes;

        public PageProfilePanel()
        {
            InitializeComponent();

            var user = entities.Users.FirstOrDefault(u => u.id_user == UserID);


            tbFIO.Text = user.fio;
            tbLogin.Text = user.login;
            tbPhone.Text = user.phone_number;
            tbNickName.Text = user.nickname;

            if (user.avatar != null)
            {
                tbChangePhoto.Text = "Изменить фото";
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(user.avatar);
                bitmap.EndInit();
                iPhotoImage.Source = bitmap;
            }
        }
        private void btEditProfile_Click(object sender, RoutedEventArgs e)
        {
            if (btEditProfile.Content.ToString() == "Изменить данные")
            {
                tbFIO.IsEnabled = true;
                tbLogin.IsEnabled = true;
                tbPhone.IsEnabled = true;
                tbNickName.IsEnabled = true;
                btAddPhoto.IsEnabled = true;
                btEditProfile.Content = "Сохранить";
            }
            else if (btEditProfile.Content.ToString() == "Сохранить")
            {
                int error = 0;
                var user = entities.Users.FirstOrDefault(u => u.id_user == UserID);

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

                try
                {
                    user.fio = tbFIO.Text;
                    user.login = tbLogin.Text;
                    user.phone_number = tbPhone.Text;
                    user.nickname = tbNickName.Text;
                    user.avatar = fileBytes;

                    entities.SaveChanges();

                    MessageBox.Show("Данные успешно обновлены!");
                    btEditProfile.Content = "Изменить данные";
                    tbFIO.IsEnabled = false;
                    tbLogin.IsEnabled = false;
                    tbPhone.IsEnabled = false;
                    tbNickName.IsEnabled = false;
                    btAddPhoto.IsEnabled = false;
                    tbChangePhoto.Text = "Изменить фото";
                    loggingSystem.LogginUserUse("Изменение данных в личном кабинете");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btEditPassword_Click(object sender, RoutedEventArgs e)
        {

            if (btEditPassword.Content.ToString() == "Изменить пароль")
            {
                editPassword.GenerateEditPassword(spPassword, spPassword);
                tbOldPassword = editPassword.tbOldPassword;
                tbNewPassword = editPassword.tbNewPassword;
                btEditPassword.Content = "Сохранить";
            }
            else if (btEditPassword.Content.ToString() == "Сохранить")
            {
                if (tbOldPassword.Text.Trim() == "" || tbNewPassword.Text.Trim() == "")
                {
                    MessageBox.Show("Изменение отменено, потому что одно из полей пусто");
                    btEditPassword.Content = "Изменить пароль";
                    editPassword.RemoveEditPassword();
                    return;
                }

                if (tbOldPassword.Text == tbNewPassword.Text)
                {
                    MessageBox.Show("Пароли совпадают! Придумайте новый!");
                    return;
                }

                var user = entities.Users.FirstOrDefault(u => u.password == tbOldPassword.Text);

                if (user != null)
                {
                    try
                    {
                        user.password = tbNewPassword.Text.Trim();
                        entities.SaveChanges();
                        MessageBox.Show("Пароль успешно изменён!");
                        loggingSystem.LogginUserUse("Изменение пароля в личном кабинете");
                        NavigationService.Navigate(new PageSingIn());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Пароль не верный!");
                }
                btEditPassword.Content = "Изменить пароль";
            }
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
            try
            {
            phoneNumber = string.Format("+7 ({0}) {1}-{2}-{3}", phoneNumber.Substring(1, 3), phoneNumber.Substring(4, 3), phoneNumber.Substring(7, Math.Min(phoneNumber.Length - 7, 2)), phoneNumber.Substring(9, Math.Min(phoneNumber.Length - 9, 2)));
            }
            catch
            {

            }

            return phoneNumber;
        }
        private void hlExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            // Создание экземпляра OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            // Отображение диалогового окна для выбора файла
            if (openFileDialog.ShowDialog() == true)
            {
                // Отображение выбранного изображения в Image элементе
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = new Uri(openFileDialog.FileName);
                bitmap.EndInit();
                iPhotoImage.Source = bitmap;

                // Чтение содержимого файла в байтовый массив
                fileBytes = File.ReadAllBytes(openFileDialog.FileName);
            }
        }
    }
}
