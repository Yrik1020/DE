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
    /// Логика взаимодействия для PageProgramPanel.xaml
    /// </summary>
    public partial class PageProgramPanel : Page
    {
        Entities entities = new Entities();
        LoggingSystem loggingSystem = new LoggingSystem();
        public PageProgramPanel()
        {
            InitializeComponent();

            dgMain.ItemsSource = entities.Line_schedule.ToList();

        }
        private void btCreate_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageAddEditUser());
        }

        private void tbExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            var item = dgMain.SelectedItems.Cast<Line_schedule>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить элемент?\nЭто может вызвать необратимые последствия!", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    foreach (var removing in item)
                    {
                        Line_schedule _item = entities.Line_schedule
                             .Where(c => c.id_line == removing.id_line)
                             .FirstOrDefault();
                        string data = $"{_item.date} - {_item.id_discipline} - {_item.id_prof} - {_item.classroom}";
                        entities.Line_schedule.Remove(_item);
                        entities.SaveChanges();
                        loggingSystem.LogginAdminUse($"Удаление строки расписания {data}");

                    }

                    MessageBox.Show("Удалено!");

                    dgMain.ItemsSource = entities.Line_schedule.ToList();
                }
                catch (Exception ex)
                {
                    // Сообщение об ошибке, если она есть
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
            Line_schedule item = new Line_schedule();
            item = (sender as Button).DataContext as Line_schedule;

            Application.Current.Properties["AddEditItemId"] = item.id_line;
            NavigationService.Navigate(new PageAddEditUser());
        }
    }
}
