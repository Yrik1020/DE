using Collage.src._pages.adminPage_;
using Collage.src._pages.messagerPage_;
using Collage.src._pages.profilePage_;
using Collage.src._pages.programPage_;
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

namespace Collage.src._pages.welcomePage_
{
    /// <summary>
    /// Логика взаимодействия для PageNavigation.xaml
    /// </summary>
    public partial class PageNavigation : Page
    {
        Entities entities = new Entities();
        private int UserID = Convert.ToInt32(Application.Current.Properties["UserID"]);
        LoggingSystem loggingSystem = new LoggingSystem();

        public PageNavigation()
        {
            InitializeComponent();
            frNavigationFrame.Navigate(new PageProfilePanel());

            var user = entities.Users.FirstOrDefault(u => u.id_user == UserID);
            string type = user.User_types.technical_status;
            if (type == "managerProgramm")
                brProgram.Visibility = Visibility.Visible;

            if (type == "admin")
            {
                brProgram.Visibility = Visibility.Visible;
                brAdmin.Visibility = Visibility.Visible;
            }
        }

        private void iProfile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            frNavigationFrame.Navigate(new PageProfilePanel());
            loggingSystem.LogginUserUse("Открыта страница профиля");
        }

        private void iTasker_MouseDown(object sender, MouseButtonEventArgs e)
        {
            frNavigationFrame.Navigate(new PageTaskPanel());
            loggingSystem.LogginUserUse("Открыта страница задач");
        }

        private void iMsg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            frNavigationFrame.Navigate(new PageMesengerePanel());
            loggingSystem.LogginUserUse("Открыта страница мессенджера");
        }

        private void iProgram_MouseDown(object sender, MouseButtonEventArgs e)
        {
            frNavigationFrame.Navigate(new programPage_.PageProgramPanel());
            loggingSystem.LogginUserUse("Открыта страница расписания");
        }

        private void iAdmin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            frNavigationFrame.Navigate(new PageAdminPanel());
            loggingSystem.LogginUserUse("Открыта страница администратора");
        }

        private void ifOff_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Получение ссылки на главное окно
            Window mainWindow = Application.Current.MainWindow;

            // Закрытие главного окна
            mainWindow.Close();
        }
    }
}
