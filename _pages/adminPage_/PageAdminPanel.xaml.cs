using Collage.src._pages.welcomePage_;
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
    /// Логика взаимодействия для PageAdminPanel.xaml
    /// </summary>
    public partial class PageAdminPanel : Page
    {
        public PageAdminPanel()
        {
            InitializeComponent();
        }

        private void btPanelUser_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageUserPanel());
        }

        private void btPanelProgram_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageProgramPanel());

        }

        private void btPanelTask_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageTaskPanel());

        }

        private void btPanelProff_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageProgramPanel());
        }

        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageSingIn());

        }
    }
}
