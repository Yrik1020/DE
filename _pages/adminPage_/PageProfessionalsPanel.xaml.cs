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

namespace Collage.src._pages.adminPage_
{
    /// <summary>
    /// Логика взаимодействия для PageProfessionalsPanel.xaml
    /// </summary>
    public partial class PageProfessionalsPanel : Page
    {
        Entities entities = new Entities();
        LoggingSystem loggingSystem = new LoggingSystem();
        public PageProfessionalsPanel()
        {
            InitializeComponent();
                        dgMain.ItemsSource = entities.Professionals.ToList();
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
            var item = dgMain.SelectedItems.Cast<Professionals>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить элемент?\nЭто может вызвать необратимые последствия!", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    foreach (var removing in item)
                    {
                        Professionals _item = entities.Professionals
                             .Where(c => c.id_professional == removing.id_professional)
                             .FirstOrDefault();

                        string data = _item.named;

                        entities.Professionals.Remove(_item);
                        entities.SaveChanges();

                        loggingSystem.LogginAdminUse($"Удаление профессии {data}");
                    }

                    MessageBox.Show("Удалено!");

                    dgMain.ItemsSource = entities.Professionals.ToList();
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
            Professionals item = new Professionals();
            item = (sender as Button).DataContext as Professionals;

            Application.Current.Properties["AddEditItemId"] = item.id_professional;
            NavigationService.Navigate(new PageAddEditUser());
        }
    }
}
