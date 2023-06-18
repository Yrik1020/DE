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

namespace Collage.src._pages.programPage_
{
    /// <summary>
    /// Логика взаимодействия для PageEditProgramPanel.xaml
    /// </summary>
    public partial class PageEditProgramPanel : Page
    {
        public PageEditProgramPanel()
        {
            InitializeComponent();
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void hlExit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            cbItemTsk.Text = "3 ИС-Б, Суббота, 1 пара, МДК 04 02, 43 каб";
        }
    }
}
