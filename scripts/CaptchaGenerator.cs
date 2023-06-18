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
    public class CaptchaGenerator
    {
        public Label labelCaptcha = new Label();
        public TextBox tbCapcha = new TextBox();
        public int length_;
        public string capchaCode;
        public void GenerateCaptcha(StackPanel stackPanel, UIElement elementName, int length)
        {
            capchaCode = $"{GenerateRandomString(length)}";
            length_ = length;
            // создаем новый Label
            Label Capcha = new Label();
            Capcha.Content = capchaCode; // Задаёт текст в виде сгенерированой капчи
            Capcha.Margin = new Thickness(0, 0, 0, 10); // Задаём margin 
            Capcha.VerticalContentAlignment = VerticalAlignment.Center; // Центрация по вертикали
            Capcha.HorizontalContentAlignment = HorizontalAlignment.Center; // Центрация по горизонтали
            Capcha.FontSize = 12; // Размер шрифта
            Capcha.FontFamily = new FontFamily("Ink Free"); // Шрифт
            Capcha.Background = new SolidColorBrush(Color.FromRgb(51, 51, 51)); // Цвет фона
            Capcha.Foreground = Brushes.White; // Цвет текста
            Capcha.Width = 70; // Размер в label в ширину
            labelCaptcha = Capcha; // Присваиваем label в публичную перменную

            // создаёт новый textbox
            tbCapcha.Margin = new Thickness(0, 0, 0, 10);
            tbCapcha.VerticalContentAlignment = VerticalAlignment.Center;
            tbCapcha.Foreground = new SolidColorBrush(Colors.White);
            tbCapcha.Width = 150;
            tbCapcha.Name = "tbCapcha"; // Название для дальнейшенй работы
            tbCapcha.FontFamily = new FontFamily("Centry");
            tbCapcha.Style = (Style)Application.Current.Resources["MaterialDesignFloatingHintTextBox"]; // Задаём стиль
            tbCapcha.SetValue(HintAssist.HintProperty, "Введите капчу"); // Текст подсказки
            tbCapcha.HorizontalContentAlignment = HorizontalAlignment.Center;

            stackPanel.Children.Insert(stackPanel.Children.IndexOf(elementName) + 1, Capcha); // Создаём новый элемент label в группировке StackPanel, под паролем
            stackPanel.Children.Insert(stackPanel.Children.IndexOf(Capcha) + 1, tbCapcha); // Добавляем TextBox под Label
        }

        public void RegenarateCaptcha(Label elementName)
        {
            // Генерация новый капчи
            capchaCode = $"{GenerateRandomString(length_)}";
            elementName.Content = capchaCode;
        }
        private string GenerateRandomString(int length)
        {
            // Генерация значений для капчи
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private Random random = new Random();
    }
}
