using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Collage.src.scripts
{
    public class EditPassword
    {
        public TextBox tbOldPassword = new TextBox();
        public TextBox tbNewPassword = new TextBox();
        private StackPanel mainStackPanel = new StackPanel();
        public void GenerateEditPassword(StackPanel stackPanel, StackPanel elementName)
        {
            // создаёт новый textbox с старым паролем
            tbOldPassword.VerticalContentAlignment = VerticalAlignment.Center;
            tbOldPassword.Width = 250;
            tbOldPassword.Style = (Style)Application.Current.Resources["MaterialDesignFloatingHintTextBox"]; // Задаём стиль
            tbOldPassword.FontFamily = new FontFamily("Centry");
            tbOldPassword.Foreground = new SolidColorBrush(Colors.White);
            tbOldPassword.SetValue(HintAssist.HintProperty, "Введите старый пароль"); // Текст подсказки
            tbOldPassword.HorizontalContentAlignment = HorizontalAlignment.Center;

            // создаёт новый textbox с новым паролем
            tbNewPassword.Margin = new Thickness(5, 0, 0, 0);
            tbNewPassword.VerticalContentAlignment = VerticalAlignment.Center;
            tbNewPassword.Width = 250;
            tbNewPassword.Foreground = new SolidColorBrush(Colors.White);
            tbNewPassword.Style = (Style)Application.Current.Resources["MaterialDesignFloatingHintTextBox"]; // Задаём стиль
            tbNewPassword.FontFamily = new FontFamily("Centry");
            tbNewPassword.SetValue(HintAssist.HintProperty, "Введите новый пароль"); // Текст подсказки
            tbNewPassword.HorizontalContentAlignment = HorizontalAlignment.Center;

            stackPanel.Children.Insert(stackPanel.Children.IndexOf(elementName) + 1, tbOldPassword); // Создаём новый элемент label в группировке StackPanel, под паролем
            stackPanel.Children.Insert(stackPanel.Children.IndexOf(tbOldPassword) + 1, tbNewPassword); // Создаём новый элемент label в группировке StackPanel, под паролем
            mainStackPanel = stackPanel;
        }

        public void RemoveEditPassword()
        {
            mainStackPanel.Children.Remove(tbOldPassword);
            mainStackPanel.Children.Remove(tbNewPassword);
        }
    }
}
