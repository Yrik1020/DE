using Collage.src.DateBase;
using Collage.src.scripts;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Collage.src._pages.adminPage_
{
    /// <summary>
    /// Логика взаимодействия для PageAddEditProfessionals.xaml
    /// </summary>
    public partial class PageAddEditProfessionals : Page
    {
        Entities entities = new Entities();
        private string statusEdit = "Edit";
        private int itemid = Convert.ToInt32(Application.Current.Properties["AddEditItemId"]);
        private Professionals _item = new Professionals(); 
        LoggingSystem loggingSystem = new LoggingSystem();

        public PageAddEditProfessionals()
        {
            InitializeComponent();

            var item = entities.Professionals.FirstOrDefault(i => i.id_professional == itemid);

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
                var item = entities.Professionals.FirstOrDefault(i => i.id_professional == itemid);

                try
                {
                    entities.SaveChanges();

                    MessageBox.Show("Успех");

                    loggingSystem.LogginAdminUse($"Обновление Профессии {item.named}");

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
                    entities.Professionals.Add(_item);
                    entities.SaveChanges();
                    // Выводим сообщение об успехе
                    MessageBox.Show("Профессия успешно добавлен в базу данных!");

                    NavigationService.Navigate(new PageUserPanel());
                }
                catch (Exception ex)
                {
                    // Выводим сообщение об ошибке, если что-то пошло не так
                    MessageBox.Show("Произошла ошибка при добавлении профессии в базу данных: " + ex.Message);
                }
            }
        }

        private void hlExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageUserPanel());
        }
    }
}
