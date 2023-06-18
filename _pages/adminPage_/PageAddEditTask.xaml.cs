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
    /// Логика взаимодействия для PageAddEditTask.xaml
    /// </summary>
    class Data
    {
        public string UserF { set; get; }
        public int TaskerID { set; get; }
        public Data(string userfio, int taskerid)
        {
            UserF = userfio;
            TaskerID = taskerid;
        }   
    }
    public partial class PageAddEditTask : Page
    {
        Entities entities = new Entities();
       
        public PageAddEditTask()
        {
            InitializeComponent();           
            List<Data> taskersXfio = new List<Data>();
           
            foreach (var tasker in entities.Taskers)
            {
                foreach (var user in entities.Users)
                {
                   if(user.id_user == tasker.id_user)
                    {
                    taskersXfio.Add(new Data(user.fio, tasker.id_tasker));
                    }
                }                
            }

            cbItemTsk.ItemsSource = taskersXfio;
            cbItemTsk.DisplayMemberPath = "UserF";
            cbItemTsk.SelectedValue = "TaskerID";

            
        }
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
        }
      

        private void hlExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageUserPanel());
        }
    }
}
