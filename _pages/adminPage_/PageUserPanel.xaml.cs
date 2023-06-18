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
    /// Логика взаимодействия для PageUserPanel.xaml
    /// </summary>
    public partial class PageUserPanel : Page
    {
        Entities entities = new Entities();
        LoggingSystem loggingSystem = new LoggingSystem();

        public PageUserPanel()
        {
            InitializeComponent();
            dgMain.ItemsSource = entities.Users.ToList();
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
            var item = dgMain.SelectedItems.Cast<Users>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить элемент?\nЭто может вызвать необратимые последствия!", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    foreach (var removing in item)
                    {
                        Users _item = entities.Users
                             .Where(c => c.id_user == removing.id_user)
                             .FirstOrDefault();

                        string fio = _item.fio;
                        entities.Users.Remove(_item);
                        entities.SaveChanges();
                        loggingSystem.LogginAdminUse($"Удаление пользователя {fio}");
                    }

                    MessageBox.Show("Удалено!");

                    dgMain.ItemsSource = entities.Users.ToList();
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
            Users item = new Users();
            item = (sender as Button).DataContext as Users;

            Application.Current.Properties["AddEditItemId"] = item.id_user;
            NavigationService.Navigate(new PageAddEditUser());
        }
    }
}
