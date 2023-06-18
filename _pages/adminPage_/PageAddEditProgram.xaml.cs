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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace Collage.src._pages.adminPage_
{
    /// <summary>
    /// Логика взаимодействия для PageAddEditProgram.xaml
    /// </summary>
    public partial class PageAddEditProgram : Page
    {
        Entities entities = new Entities();
        private string statusEdit = "Edit";
        private int itemid = Convert.ToInt32(Application.Current.Properties["AddEditItemId"]);
        private Line_schedule _item = new Line_schedule();
        LoggingSystem loggingSystem = new LoggingSystem();
        public PageAddEditProgram()
        {
            InitializeComponent();


            var item = entities.Line_schedule.FirstOrDefault(i => i.id_line == itemid);

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

            cbItemDisc.ItemsSource = entities.Discipline.ToList();
            cbItemProf.ItemsSource = entities.Professionals.ToList();
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
            if (cbItemProf.SelectedItem == null || cbItemDisc.SelectedItem == null)
                error++;
            // Если есть пустые поля вывод сообщения
            if (error >= 1)
            {
                MessageBox.Show("Не все поля заполнены и не все элементы выбраны!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (statusEdit == "Edit")
            {
                var item = entities.Line_schedule.FirstOrDefault(i => i.id_line == itemid);

                try
                {
                    entities.SaveChanges();

                    MessageBox.Show("Успех");

                    loggingSystem.LogginAdminUse($"Обновление расписания {item.Professionals.named}");


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
                    entities.Line_schedule.Add(_item);
                    entities.SaveChanges();
                    // Выводим сообщение об успехе
                    MessageBox.Show("Расписание успешно добавлен в базу данных!");
                    loggingSystem.LogginAdminUse($"Добавление расписания {_item.Professionals.named}");

                    NavigationService.Navigate(new PageUserPanel());
                }
                catch (Exception ex)
                {
                    // Выводим сообщение об ошибке, если что-то пошло не так
                    MessageBox.Show("Произошла ошибка при добавлении расписания в базу данных: " + ex.Message);
                }
            }
        }

        private void hlExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageUserPanel());
        }
    }
}
