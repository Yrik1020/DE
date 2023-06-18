using Collage.src.DateBase;
using Collage.src.scripts;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Collage.src._pages.messagerPage_
{
    /// <summary>
    /// Логика взаимодействия для PageMesengerePanel.xaml
    /// </summary>
    public partial class PageMesengerePanel : Page
    {
        Entities entities = new Entities();
        private int UserID = Convert.ToInt32(Application.Current.Properties["UserID"]);
        private int ChatingId;
        public PageMesengerePanel()
        {
            InitializeComponent();

            tileListView.ItemsSource = entities.Users.Where(u => u.id_user != UserID).ToList();
        }



        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Получение данных собеседника из привязки
            var dataContext = ((FrameworkElement)sender).DataContext;
            var conversation = dataContext as Users;
            if (conversation != null)
            {
                var conversationId = conversation.id_user;
                // Выполните нужные действия с айди собеседника
                var userFrom = entities.Users.FirstOrDefault(u => u.id_user == UserID);
                var userTo = entities.Users.FirstOrDefault(u => u.id_user == conversationId);

                // Создаем и отображаем красивое окно с вариантами
                lbToChattings.Content = $"Переписка с - {userTo.fio}";

                stMainChat.Children.RemoveRange(1, stMainChat.Children.Count - 1);

                ManagerChating managerChating = new ManagerChating();

                bSendMessage.Visibility= Visibility.Visible;

                var chat_ = entities.Chatings.FirstOrDefault(c => c.id_user_one == UserID && c.id_user_two == conversationId);

                if (chat_ == null)
                {
                    Chatings newChatings = new Chatings();

                    newChatings.id_user_one = UserID;
                    newChatings.id_user_two = conversationId;

                    try
                    {
                        entities.Chatings.Add(newChatings);
                        entities.SaveChanges();

                        chat_ = newChatings;
                    }catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                managerChating.GeneralGenericMessage(stMainChat, chat_.id_chat, lbStart);
                ChatingId = chat_.id_chat;
            }
        }

        private void btSendMessage_Click(object sender, MouseButtonEventArgs e)
        {
            Line_chatings line_Chatings = new Line_chatings();

            line_Chatings.message = tbSendMessage.Text;
            line_Chatings.datetime = DateTime.Now;
            line_Chatings.id_user = UserID;
            line_Chatings.id_chatings = ChatingId;

            try
            {
                entities.Line_chatings.Add(line_Chatings);
                entities.SaveChanges();

                tbSendMessage.Text = "";
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btSendFile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Line_chatings line_Chatings = new Line_chatings();
            line_Chatings.datetime = DateTime.Now;
            line_Chatings.id_user = UserID;
            line_Chatings.id_chatings = ChatingId;

            // Создание экземпляра OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Отображение диалогового окна для выбора файла
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                string fileExtension = System.IO.Path.GetExtension(fileName);

                // Чтение содержимого файла в байтовый массив
                byte[] fileBytes = File.ReadAllBytes(fileName);

                line_Chatings.send_file = fileBytes;
                line_Chatings.send_file_name = System.IO.Path.GetFileName(fileName);

                try
                {
                    entities.Line_chatings.Add(line_Chatings);
                    entities.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        private void btSendImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Line_chatings line_Chatings = new Line_chatings();

            line_Chatings.datetime = DateTime.Now;
            line_Chatings.id_user = UserID;
            
            line_Chatings.id_chatings = ChatingId;
            // Создание экземпляра OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            byte[] fileBytes;

            // Отображение диалогового окна для выбора файла
            if (openFileDialog.ShowDialog() == true)
            {
                // Отображение выбранного изображения в Image элементе
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = new Uri(openFileDialog.FileName);
                bitmap.EndInit();

                // Чтение содержимого файла в байтовый массив
                fileBytes = File.ReadAllBytes(openFileDialog.FileName);
                line_Chatings.image = fileBytes;
            }

            try
            {
                entities.Line_chatings.Add(line_Chatings);
                entities.SaveChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void OnClick(object sender, RoutedEventArgs e)
        {
            Line_chatings line_Chatings = new Line_chatings();

            line_Chatings = (sender as Hyperlink).DataContext as Line_chatings;

            // Получение содержимого файла в форме varbinary(max)
            byte[] fileContent = line_Chatings.send_file;

            // Открытие диалогового окна сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = line_Chatings.send_file_name; // Установка начального имени файла
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                // Сохранение содержимого файла по указанному пути
                File.WriteAllBytes(filePath, fileContent);
            }
        }
        private void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            ScrollViewer scrollViewer = sender as ScrollViewer;
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToEnd();
                scrollViewer.ScrollChanged += (s, args) =>
                {
                    if (args.ExtentHeightChange != 0)
                    {
                        scrollViewer.ScrollToEnd();
                    }
                };
            }
        }

    }

    public class FioToInitialsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string fio = value as string;
            if (string.IsNullOrEmpty(fio))
                return string.Empty;

            string[] names = fio.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (names.Length == 0)
                return string.Empty;

            string lastName = names[0];
            string initials = lastName;
            if (names.Length > 1)
            {
                for (int i = 1; i < names.Length - 1; i++)
                {
                    string namePart = names[i];
                    initials += " " + namePart.Substring(0, 1) + ".";
                }
                initials += " " + names[names.Length - 1].Substring(0, 1) + ".";
            }

            return initials;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class AvatarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Проверяем, является ли значение null
            if (value == null)
            {
                // Возвращаем путь к заготовленной фотографии
                return "/src/sourse/images/placeholder.png";
            }

            // Возвращаем путь к фактической фотографии
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
    
}
