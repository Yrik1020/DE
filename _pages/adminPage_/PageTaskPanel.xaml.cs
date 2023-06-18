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
    /// Логика взаимодействия для PageTaskPanel.xaml
    /// </summary>
    public partial class PageTaskPanel : Page
    {
        
        Entities entities = new Entities();
        LoggingSystem loggingSystem = new LoggingSystem();
        public PageTaskPanel()
        {
            InitializeComponent();
            dgMain.ItemsSource = entities.line_taskers.ToList(); 
        }
        private void btCreate_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageAddEditTask());
        }
        private void tbExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
