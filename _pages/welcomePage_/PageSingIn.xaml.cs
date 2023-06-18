using Collage.src._pages.adminPage_;
using Collage.src._pages.messagerPage_;
using Collage.src._pages.profilePage_;
using Collage.src._pages.programPage_;
using Collage.src.DateBase;
using Collage.src.scripts;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Collage.src._pages.welcomePage_
{
    /// <summary>
    /// Логика взаимодействия для PageSingIn.xaml
    /// </summary>
    public partial class PageSingIn : Page
    {
        private Entities entities = new Entities();
        private int count = 0;
        private TextBox lbCaptcha = new TextBox();
        // Инициализируем класс CaptchaGenerator
        public CaptchaGenerator captchaGenerator = new CaptchaGenerator();
        // Создаём пустой TextBox куда потом поместим текстбокс для капчи
        public TextBox tbCaptcha = new TextBox();
        // Установите значения ключа и вектора инициализации
        private static byte[] key = { 0x2B, 0x7E, 0x15, 0x16, 0x28, 0xAE, 0xD2, 0xA6, 0xAB, 0xF7, 0x15, 0x88, 0x09, 0xCF, 0x4F, 0x3C };
        private static byte[] iv = { 0x32, 0x88, 0x31, 0xE0, 0x37, 0x43, 0x6E, 0xA8, 0x5F, 0x8D, 0xA2, 0x33, 0xB3, 0xFE, 0x68, 0x7C };
        LoggingSystem loggingSystem = new LoggingSystem();
        public PageSingIn()
        {
            InitializeComponent();

            // Проверка наличия сохраненных данных авторизации
            string filePath = "credentials.txt";
            if (File.Exists(filePath))
            {
                string credentials = EncryptionHelper.DecryptData(filePath);
                string[] parts = credentials.Split(',');
                string saveUserLogin = parts[0];
                string saveUserPassword = parts[1];

                tbLogin.Text = saveUserLogin;
                tbPassword.Password = saveUserPassword;

                loggingSystem.LogginSystem("Вход по системе сохранения логина и пароля");

                cbRememberMe.IsChecked = true;

                return;
            }
        }
        private bool validate;
        public void Authentication(string login, string password)
        {
            Entities nentities = new Entities();
            entities = nentities;
            // Ищем единственно верного пользователя по значениям login и password
            var user = entities.Users.FirstOrDefault(u => u.login == login && u.password == password);

            // Провка существует ли пользователь
            if (user != null)
            {
                validate = true;
                MessageBox.Show("Успешная авторизация");
                Application.Current.Properties["UserID"] = user.id_user;
                Application.Current.Properties["NickName"] = user.nickname;
                count = 0;

                string fullName = user.fio;
                string[] names = fullName.Split(' ');
                string lastName = names[0];
                string initials = "";

                for (int i = 1; i < names.Length; i++)
                {
                    if (names[i] != "") // пропускаем отсутствующие отчества
                    {
                        initials += names[i][0] + ". ";
                    }
                }
                Application.Current.Properties["FIO"] = lastName + " " + initials;

                // Проверка состояния CheckBox
                bool rememberMe = cbRememberMe.IsChecked ?? false;
                // Если CheckBox выбран, сохраните данные авторизации
                if (rememberMe)
                {
                    string userLogin = user.login; // Получить айди пользователя
                    string userPassword = user.password; // Получить ФИО пользователя

                    // Сохраните данные авторизации в безопасное хранилище
                    // Например, сохраните их в файл
                    string filePath = "credentials.txt";
                    string credentials = $"{userLogin},{userPassword}";
                    EncryptionHelper.EncryptData(credentials, filePath);
                    //File.WriteAllText(filePath, credentials);
                    loggingSystem.LogginSystem("Создание зашифрованного файла авторизации");
                }

                OpenPage(user.User_types.technical_status);
                return;
            }

            MessageBox.Show("Ошибка авторизации");

            loggingSystem.LogginSystem("Ошибка авторизации");
        }

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text.Trim();
            string password = tbPassword.Password.Trim();
            count++;
            if (count == 2)
            {
                // Создаём СтакПанель и ложим в неё стак панель с x:Name = "mainStackPanel"
                StackPanel stackPanel = FindName("mainStackPanel") as StackPanel;

                // В переменную TextBox записываем созданный TextBox и указываем стак панель, TextBox после которого создовать капчу и длину капчи
                captchaGenerator.GenerateCaptcha(stackPanel, tbPassword, 4);
                // Добавляем новый TextBox в переменную для дальнейшего использования
                tbCaptcha = captchaGenerator.tbCapcha;
                // Остановка кода
                return;
            }

            if (count >= 3)
            {
                if (tbCaptcha.Text.Trim() != captchaGenerator.capchaCode)
                {
                    MessageBox.Show("Капча введена не верно!");
                    captchaGenerator.RegenarateCaptcha(captchaGenerator.labelCaptcha);
                    loggingSystem.LogginSystem("Невернно введённая капча");
                    return;
                }
            }

            // Вызов метода проверки логина и пароля
            Authentication(login, password);
        }
        async void SetTimer()
        {
            btLogin.IsEnabled = false;
            await Task.Delay(60000);
            btLogin.IsEnabled = true;
        }

        private void OpenPage(string nameType)
        {
            // Если логин admin, то выводит панель администратора

            NavigationService.Navigate(new PageNavigation());
            loggingSystem.LogginSystem("Открытие страницы корпоративной сети");

        }
        private void hlExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageRegistration());
        }

        private void cbRememberMe_Unchecked(object sender, RoutedEventArgs e)
        {
            if (cbRememberMe.IsChecked == false)
            {
                string filePath = "credentials.txt";
                // Проверяем, существует ли файл
                if (File.Exists(filePath))
                {
                    // Удаляем файл
                    File.Delete(filePath);
                    loggingSystem.LogginSystem("Убрана галочка на сохранении логина и пароля для авторизации");
                    loggingSystem.LogginSystem("Удаление файла авторизации");
                }
            }
        }
    }
}
