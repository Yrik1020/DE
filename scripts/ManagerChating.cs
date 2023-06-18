using Collage.src.DateBase;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace Collage.src.scripts
{
    public class ManagerChating
    {
        Entities entities = new Entities();
        private int UserID = Convert.ToInt32(Application.Current.Properties["UserID"]);
        private StackPanel stackPanelTemp = new StackPanel();
        private DateTime dateTimeTemp = new DateTime();
        private int messageTemp = 0;
        public void GeneralGenericMessage(StackPanel stackPanel, int idChating, UIElement uIElement)
        {
            var msgs = entities.Line_chatings.Where(msg => msg.id_chatings == idChating).ToList();
            int count = 0;
            foreach (var msg in msgs)
            {
                if (count >= 1)
                {
                    if (msg.id_user == UserID)
                    {
                        count++;
                        if (msg.image != null)
                        {
                            GeneralMessageImageTo(msg.Users.nickname, msg.message, msg, Convert.ToDateTime(msg.datetime), stackPanel, stackPanelTemp);
                        }
                        else if (msg.send_file != null)
                            GeneralMessageFileTo(msg.Users.nickname, msg.message, msg, Convert.ToDateTime(msg.datetime), stackPanel, stackPanelTemp);
                        else
                            GeneralStackPanelTo(msg.Users.nickname, msg.message, msg, Convert.ToDateTime(msg.datetime), stackPanel, stackPanelTemp);
                    }
                    else
                    {
                        if (msg.image != null)
                        {
                            GeneralMessageImageFrom(msg.Users.nickname, msg.message, msg, Convert.ToDateTime(msg.datetime), stackPanel, stackPanelTemp);
                        }
                        else if (msg.send_file!= null)
                            GeneralMessageFileFrom(msg.Users.nickname, msg.message, msg, Convert.ToDateTime(msg.datetime), stackPanel, stackPanelTemp);
                        else
                        {
                            GeneralStackPanelFrom(msg.Users.nickname, msg.message, msg, Convert.ToDateTime(msg.datetime), stackPanel, stackPanelTemp);
                        }
                        count++;
                    }
                }
                else
                {
                    if (msg.id_user == UserID)
                    {
                        count++;
                        if (msg.image != null)
                        {
                            GeneralMessageImageTo(msg.Users.nickname, msg.message, msg, Convert.ToDateTime(msg.datetime), stackPanel, stackPanelTemp);
                        }
                        else if (msg.send_file != null)
                            GeneralMessageFileTo(msg.Users.nickname, msg.message, msg, Convert.ToDateTime(msg.datetime), stackPanel, stackPanelTemp);
                        else
                            GeneralStackPanelTo(msg.Users.nickname, msg.message, msg, Convert.ToDateTime(msg.datetime), stackPanel, uIElement);
                    }
                    else
                    {
                        count++;
                        if (msg.image != null)
                        {
                            GeneralMessageImageFrom(msg.Users.nickname, msg.message, msg, Convert.ToDateTime(msg.datetime), stackPanel, stackPanelTemp);
                        }
                        else if (msg.send_file != null)
                            GeneralMessageFileFrom(msg.Users.nickname, msg.message, msg, Convert.ToDateTime(msg.datetime), stackPanel, stackPanelTemp);
                        else
                            GeneralStackPanelFrom(msg.Users.nickname, msg.message, msg, Convert.ToDateTime(msg.datetime), stackPanel, uIElement);
                    }
                }
            }
            GeneralGenericMessageRefreh(stackPanel, idChating, uIElement);
        }
        async private void GeneralGenericMessageRefreh(StackPanel stackPanel, int idChating, UIElement uIElement)
        {
            while (true)
            {
                await Task.Delay(1000);

                var msgsNow = entities.Line_chatings.Where(msg => msg.id_chatings == idChating && msg.id_line != messageTemp && msg.datetime > DateTime.Now);

                if (msgsNow != null)
                {
                    foreach (var msg in msgsNow)
                    {
                        if (msg.id_user == UserID)
                        {
                            if (msg.image != null)
                            {
                                GeneralMessageImageTo(msg.Users.nickname, msg.message, msg, Convert.ToDateTime(msg.datetime), stackPanel, stackPanelTemp);
                            }
                            else if (msg.send_file != null)
                                GeneralMessageFileTo(msg.Users.nickname, msg.message, msg, Convert.ToDateTime(msg.datetime), stackPanel, stackPanelTemp);
                            else
                            {
                                GeneralStackPanelTo(msg.Users.nickname, msg.message, msg, Convert.ToDateTime(msg.datetime), stackPanel, stackPanelTemp);
                            }
                        }
                        else
                        {
                            if (msg.image != null)
                            {
                                GeneralMessageImageFrom(msg.Users.nickname, msg.message, msg, Convert.ToDateTime(msg.datetime), stackPanel, stackPanelTemp);
                            }
                            else if (msg.send_file != null)
                                GeneralMessageFileFrom(msg.Users.nickname, msg.message, msg, Convert.ToDateTime(msg.datetime), stackPanel, stackPanelTemp);
                            else
                            {
                                GeneralStackPanelFrom(msg.Users.nickname, msg.message, msg, Convert.ToDateTime(msg.datetime), stackPanel, stackPanelTemp);
                            }
                        }
                    }
                }
            }
        }

        private void GeneralMessageImageFrom(string nickName, string message, Line_chatings msg, DateTime dateTime, StackPanel stackPanel, UIElement uIElement)
        {
            StackPanel stackPanelFrom = new StackPanel();

            stackPanelFrom.Orientation = Orientation.Vertical;
            stackPanelFrom.HorizontalAlignment = HorizontalAlignment.Left;
            stackPanelFrom.Margin = new Thickness(5);

            Label label = new Label();

            label.HorizontalContentAlignment = HorizontalAlignment.Left;
            label.Style = (Style)Application.Current.Resources["MaterialDesignLabel"];
            label.FontSize = 15;
            label.FontWeight = FontWeights.Bold;
            label.Foreground = Brushes.Black;
            label.Margin = new Thickness(0, 0, 0, 5);
            label.Content = nickName;

            Border borderFrom = new Border();
            borderFrom.Background = (Brush)new BrushConverter().ConvertFrom("#128C7E");
            borderFrom.CornerRadius = new CornerRadius(5);
            borderFrom.Margin = new Thickness(2);
            borderFrom.HorizontalAlignment = HorizontalAlignment.Left;

            Image imageFrom = new Image();

            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream memoryStream = new MemoryStream(msg.image))
            {
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
            }

            // Отображение данных в элементе Image
            imageFrom.Source = bitmapImage;
            imageFrom.Height = 100;

            TextBlock textBlockFrom = new TextBlock();

            textBlockFrom.TextWrapping = TextWrapping.Wrap;
            textBlockFrom.Width = double.NaN;
            textBlockFrom.Text = message;
            textBlockFrom.Foreground = Brushes.White;
            textBlockFrom.Margin = new Thickness(5);

            string formattedString;

            if (dateTime.Date == DateTime.Today)
            {
                // Если дата совпадает с текущим днём, отображаем только время
                formattedString = dateTime.ToString("HH:mm:ss");
            }
            else
            {
                // Иначе отображаем полную дату и время
                formattedString = dateTime.ToString("dd.MM.yyyy HH:mm:ss");
            }

            Label datetime_ = new Label();
            datetime_.HorizontalContentAlignment = HorizontalAlignment.Left;
            datetime_.Style = (Style)Application.Current.Resources["MaterialDesignLabel"];
            datetime_.FontSize = 10;
            datetime_.Foreground = Brushes.Black;
            datetime_.Margin = new Thickness(0, 0, 0, 0);
            datetime_.Content = formattedString;

            //stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(uIElement) +1, label);
            //stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(label) +1, borderFrom);
            //stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(label) +1, textBlockFrom);
            //stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(textBlockFrom) +1, datetime_);

            stackPanel.Children.Insert(stackPanel.Children.IndexOf(uIElement) + 1, stackPanelFrom);
            stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(uIElement) + 1, label);
            stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(label) + 1, borderFrom);
            borderFrom.Child = imageFrom;
            stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(borderFrom) + 1, datetime_);

            messageTemp = msg.id_line;
            dateTimeTemp = dateTime;
            stackPanelTemp = stackPanelFrom;
        }

        private void GeneralMessageImageTo(string nickName, string message, Line_chatings msg, DateTime dateTime, StackPanel stackPanel, UIElement uIElement)
        {
            StackPanel stackPanelFrom = new StackPanel();

            stackPanelFrom.Orientation = Orientation.Vertical;
            stackPanelFrom.HorizontalAlignment = HorizontalAlignment.Right;
            stackPanelFrom.Margin = new Thickness(5);

            Label label = new Label();

            label.HorizontalContentAlignment = HorizontalAlignment.Right;
            label.Style = (Style)Application.Current.Resources["MaterialDesignLabel"];
            label.FontSize = 15;
            label.FontWeight = FontWeights.Bold;
            label.Foreground = Brushes.Black;
            label.Margin = new Thickness(0, 0, 0, 5);
            label.Content = "Вы";

            Border borderFrom = new Border();
            borderFrom.Background = (Brush)new BrushConverter().ConvertFrom("#5986a4");
            borderFrom.CornerRadius = new CornerRadius(5);

            borderFrom.Margin = new Thickness(2);
            borderFrom.HorizontalAlignment = HorizontalAlignment.Right;

            Image imageFrom = new Image();

            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream memoryStream = new MemoryStream(msg.image))
            {
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
            }

            // Отображение данных в элементе Image
            imageFrom.Source = bitmapImage;
            imageFrom.Height = 100;
            TextBlock textBlockFrom = new TextBlock();

            textBlockFrom.TextWrapping = TextWrapping.Wrap;
            textBlockFrom.Width = double.NaN;
            textBlockFrom.Text = message;
            textBlockFrom.HorizontalAlignment = HorizontalAlignment.Right;
            textBlockFrom.Foreground = Brushes.White;
            textBlockFrom.Margin = new Thickness(5);

            string formattedString;

            if (dateTime.Date == DateTime.Today)
            {
                // Если дата совпадает с текущим днём, отображаем только время
                formattedString = dateTime.ToString("HH:mm:ss");
            }
            else
            {
                // Иначе отображаем полную дату и время
                formattedString = dateTime.ToString("dd.MM.yyyy HH:mm:ss");
            }

            Label datetime_ = new Label();
            datetime_.HorizontalContentAlignment = HorizontalAlignment.Right;
            datetime_.Style = (Style)Application.Current.Resources["MaterialDesignLabel"];
            datetime_.FontSize = 10;
            datetime_.Foreground = Brushes.Black;
            datetime_.Margin = new Thickness(0, 0, 0, 0);
            datetime_.Content = formattedString;

            stackPanel.Children.Insert(stackPanel.Children.IndexOf(uIElement) + 1, stackPanelFrom);
            stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(uIElement) + 1, label);
            stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(label) + 1, borderFrom);
            borderFrom.Child = imageFrom;
            stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(borderFrom) + 1, datetime_);

            messageTemp = msg.id_line;
            dateTimeTemp = dateTime;
            stackPanelTemp = stackPanelFrom;
        }

        private void GeneralMessageFileFrom(string nickName, string message, Line_chatings msg, DateTime dateTime, StackPanel stackPanel, UIElement uIElement)
        {
            StackPanel stackPanelFrom = new StackPanel();

            stackPanelFrom.Orientation = Orientation.Vertical;
            stackPanelFrom.HorizontalAlignment = HorizontalAlignment.Left;
            stackPanelFrom.Margin = new Thickness(5);

            Label label = new Label();

            label.HorizontalContentAlignment = HorizontalAlignment.Left;
            label.Style = (Style)Application.Current.Resources["MaterialDesignLabel"];
            label.FontSize = 15;
            label.FontWeight = FontWeights.Bold;
            label.Foreground = Brushes.Black;
            label.Margin = new Thickness(0, 0, 0, 5);
            label.Content = nickName;

            Border borderFrom = new Border();
            borderFrom.Background = (Brush)new BrushConverter().ConvertFrom("#128C7E");
            borderFrom.CornerRadius = new CornerRadius(5);
            borderFrom.Margin = new Thickness(2);
            borderFrom.HorizontalAlignment = HorizontalAlignment.Left;

            TextBlock fileLabel = new TextBlock();
            fileLabel.FontSize = 15;
            fileLabel.Margin = new Thickness(5);
            fileLabel.HorizontalAlignment = HorizontalAlignment.Right;

            Hyperlink hyperlink = new Hyperlink();
            hyperlink.Inlines.Add($"{msg.send_file_name}");
            hyperlink.Foreground = Brushes.White;
            hyperlink.Click += delegate
            {
                Line_chatings line_Chatings = new Line_chatings();

                line_Chatings = msg;

                // Получение содержимого файла в форме varbinary(max)
                byte[] fileContent = line_Chatings.send_file;

                // Открытие диалогового окна сохранения файла
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = line_Chatings.send_file_name; // Установка начального имени файла
                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;

                    // Сохранение содержимого файла по указанному пути
                    File.WriteAllBytes(filePath, fileContent);
                }
            };
            //hyperlink.AddHandler(Hyperlink.ClickEvent, new RoutedEventHandler(OnClick));
            fileLabel.Inlines.Add(hyperlink);

            TextBlock textBlockFrom = new TextBlock();

            textBlockFrom.TextWrapping = TextWrapping.Wrap;
            textBlockFrom.Width = double.NaN;
            textBlockFrom.Text = message;
            textBlockFrom.Foreground = Brushes.White;
            textBlockFrom.Margin = new Thickness(5);

            string formattedString;

            if (dateTime.Date == DateTime.Today)
            {
                // Если дата совпадает с текущим днём, отображаем только время
                formattedString = dateTime.ToString("HH:mm:ss");
            }
            else
            {
                // Иначе отображаем полную дату и время
                formattedString = dateTime.ToString("dd.MM.yyyy HH:mm:ss");
            }

            Label datetime_ = new Label();
            datetime_.HorizontalContentAlignment = HorizontalAlignment.Left;
            datetime_.Style = (Style)Application.Current.Resources["MaterialDesignLabel"];
            datetime_.FontSize = 10;
            datetime_.Foreground = Brushes.Black;
            datetime_.Margin = new Thickness(0, 0, 0, 0);
            datetime_.Content = formattedString;

            //stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(uIElement) +1, label);
            //stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(label) +1, borderFrom);
            //stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(label) +1, textBlockFrom);
            //stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(textBlockFrom) +1, datetime_);

            stackPanel.Children.Insert(stackPanel.Children.IndexOf(uIElement) + 1, stackPanelFrom);
            stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(uIElement) + 1, label);
            stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(label) + 1, borderFrom);
            borderFrom.Child = fileLabel;
            stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(borderFrom) + 1, datetime_);

            messageTemp = msg.id_line;
            dateTimeTemp = dateTime;
            stackPanelTemp = stackPanelFrom;
        }

        private void GeneralMessageFileTo(string nickName, string message, Line_chatings msg, DateTime dateTime, StackPanel stackPanel, UIElement uIElement)
        {
            StackPanel stackPanelFrom = new StackPanel();

            stackPanelFrom.Orientation = Orientation.Vertical;
            stackPanelFrom.HorizontalAlignment = HorizontalAlignment.Right;
            stackPanelFrom.Margin = new Thickness(5);

            Label label = new Label();

            label.HorizontalContentAlignment = HorizontalAlignment.Right;
            label.Style = (Style)Application.Current.Resources["MaterialDesignLabel"];
            label.FontSize = 15;
            label.FontWeight = FontWeights.Bold;
            label.Foreground = Brushes.Black;
            label.Margin = new Thickness(0, 0, 0, 5);
            label.Content = "Вы";

            Border borderFrom = new Border();
            borderFrom.Background = (Brush)new BrushConverter().ConvertFrom("#5986a4");
            borderFrom.CornerRadius = new CornerRadius(5);

            borderFrom.Margin = new Thickness(2);
            borderFrom.HorizontalAlignment = HorizontalAlignment.Right;

            TextBlock fileLabel = new TextBlock();
            fileLabel.FontSize = 15;
            fileLabel.Margin = new Thickness(5);
            fileLabel.HorizontalAlignment = HorizontalAlignment.Right;

            Hyperlink hyperlink = new Hyperlink();
            hyperlink.Inlines.Add($"{msg.send_file_name}");
            hyperlink.Foreground = Brushes.White;
            hyperlink.Click += delegate
            {
                Line_chatings line_Chatings = new Line_chatings();

                line_Chatings = msg;

                // Получение содержимого файла в форме varbinary(max)
                byte[] fileContent = line_Chatings.send_file;

                // Открытие диалогового окна сохранения файла
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = line_Chatings.send_file_name; // Установка начального имени файла
                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;

                    // Сохранение содержимого файла по указанному пути
                    File.WriteAllBytes(filePath, fileContent);
                }
            };
            //hyperlink.AddHandler(Hyperlink.ClickEvent, new RoutedEventHandler(OnClick));
            fileLabel.Inlines.Add(hyperlink);

            TextBlock textBlockFrom = new TextBlock();

            textBlockFrom.TextWrapping = TextWrapping.Wrap;
            textBlockFrom.Width = double.NaN;
            textBlockFrom.Text = message;
            textBlockFrom.HorizontalAlignment = HorizontalAlignment.Right;
            textBlockFrom.Foreground = Brushes.White;
            textBlockFrom.Margin = new Thickness(5);

            string formattedString;

            if (dateTime.Date == DateTime.Today)
            {
                // Если дата совпадает с текущим днём, отображаем только время
                formattedString = dateTime.ToString("HH:mm:ss");
            }
            else
            {
                // Иначе отображаем полную дату и время
                formattedString = dateTime.ToString("dd.MM.yyyy HH:mm:ss");
            }

            Label datetime_ = new Label();
            datetime_.HorizontalContentAlignment = HorizontalAlignment.Right;
            datetime_.Style = (Style)Application.Current.Resources["MaterialDesignLabel"];
            datetime_.FontSize = 10;
            datetime_.Foreground = Brushes.Black;
            datetime_.Margin = new Thickness(0, 0, 0, 0);
            datetime_.Content = formattedString;

            stackPanel.Children.Insert(stackPanel.Children.IndexOf(uIElement) + 1, stackPanelFrom);
            stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(uIElement) + 1, label);
            stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(label) + 1, borderFrom);
            borderFrom.Child = fileLabel;
            stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(borderFrom) + 1, datetime_);

            messageTemp = msg.id_line;
            dateTimeTemp = dateTime;
            stackPanelTemp = stackPanelFrom;
        }
        private void GeneralStackPanelFrom(string nickName, string message, Line_chatings msg, DateTime dateTime, StackPanel stackPanel, UIElement uIElement)
        {
            StackPanel stackPanelFrom = new StackPanel();

            stackPanelFrom.Orientation = Orientation.Vertical;
            stackPanelFrom.HorizontalAlignment = HorizontalAlignment.Left;
            stackPanelFrom.Margin = new Thickness(5);

            Label label = new Label();

            label.HorizontalContentAlignment = HorizontalAlignment.Left;
            label.Style = (Style)Application.Current.Resources["MaterialDesignLabel"];
            label.FontSize = 15;
            label.FontWeight = FontWeights.Bold;
            label.Foreground = Brushes.Black;
            label.Margin = new Thickness(0, 0, 0, 5);
            label.Content = nickName;

            Border borderFrom = new Border();
            borderFrom.Background = (Brush)new BrushConverter().ConvertFrom("#128C7E");
            borderFrom.CornerRadius = new CornerRadius(5);
            borderFrom.Margin = new Thickness(2);
            borderFrom.HorizontalAlignment = HorizontalAlignment.Left;

            TextBlock textBlockFrom = new TextBlock();

            textBlockFrom.TextWrapping = TextWrapping.Wrap;
            textBlockFrom.Width = double.NaN;
            textBlockFrom.Text = message;
            textBlockFrom.Foreground = Brushes.White;
            textBlockFrom.Margin = new Thickness(5);

            string formattedString;

            if (dateTime.Date == DateTime.Today)
            {
                // Если дата совпадает с текущим днём, отображаем только время
                formattedString = dateTime.ToString("HH:mm:ss");
            }
            else
            {
                // Иначе отображаем полную дату и время
                formattedString = dateTime.ToString("dd.MM.yyyy HH:mm:ss");
            }

            Label datetime_ = new Label();
            datetime_.HorizontalContentAlignment = HorizontalAlignment.Left;
            datetime_.Style = (Style)Application.Current.Resources["MaterialDesignLabel"];
            datetime_.FontSize = 10;
            datetime_.Foreground = Brushes.Black;
            datetime_.Margin = new Thickness(0, 0, 0, 0);
            datetime_.Content = formattedString;

            //stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(uIElement) +1, label);
            //stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(label) +1, borderFrom);
            //stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(label) +1, textBlockFrom);
            //stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(textBlockFrom) +1, datetime_);

            stackPanel.Children.Insert(stackPanel.Children.IndexOf(uIElement) + 1, stackPanelFrom);
            stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(uIElement) + 1, label);
            stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(label) + 1, borderFrom);
            borderFrom.Child = textBlockFrom;
            stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(borderFrom) + 1, datetime_);

            messageTemp = msg.id_line;
            dateTimeTemp = dateTime;
            stackPanelTemp = stackPanelFrom;
        }

        private void GeneralStackPanelTo(string nickName, string message, Line_chatings msg, DateTime dateTime, StackPanel stackPanel, UIElement uIElement)
        {
            StackPanel stackPanelFrom = new StackPanel();

            stackPanelFrom.Orientation = Orientation.Vertical;
            stackPanelFrom.HorizontalAlignment = HorizontalAlignment.Right;
            stackPanelFrom.Margin = new Thickness(5);

            Label label = new Label();

            label.HorizontalContentAlignment = HorizontalAlignment.Right;
            label.Style = (Style)Application.Current.Resources["MaterialDesignLabel"];
            label.FontSize = 15;
            label.FontWeight = FontWeights.Bold;
            label.Foreground = Brushes.Black;
            label.Margin = new Thickness(0, 0, 0, 5);
            label.Content = "Вы";

            Border borderFrom = new Border();
            borderFrom.Background = (Brush)new BrushConverter().ConvertFrom("#5986a4");
            borderFrom.CornerRadius = new CornerRadius(5);

            borderFrom.Margin = new Thickness(2);
            borderFrom.HorizontalAlignment = HorizontalAlignment.Right;

            TextBlock textBlockFrom = new TextBlock();

            textBlockFrom.TextWrapping = TextWrapping.Wrap;
            textBlockFrom.Width = double.NaN;
            textBlockFrom.Text = message;
            textBlockFrom.HorizontalAlignment = HorizontalAlignment.Right;
            textBlockFrom.Foreground = Brushes.White;
            textBlockFrom.Margin = new Thickness(5);

            string formattedString;

            if (dateTime.Date == DateTime.Today)
            {
                // Если дата совпадает с текущим днём, отображаем только время
                formattedString = dateTime.ToString("HH:mm:ss");
            }
            else
            {
                // Иначе отображаем полную дату и время
                formattedString = dateTime.ToString("dd.MM.yyyy HH:mm:ss");
            }

            Label datetime_ = new Label();
            datetime_.HorizontalContentAlignment = HorizontalAlignment.Right;
            datetime_.Style = (Style)Application.Current.Resources["MaterialDesignLabel"];
            datetime_.FontSize = 10;
            datetime_.Foreground = Brushes.Black;
            datetime_.Margin = new Thickness(0, 0, 0, 0);
            datetime_.Content = formattedString;

            stackPanel.Children.Insert(stackPanel.Children.IndexOf(uIElement) + 1, stackPanelFrom);
            stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(uIElement) + 1, label);
            stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(label) + 1, borderFrom);
            borderFrom.Child = textBlockFrom;
            stackPanelFrom.Children.Insert(stackPanelFrom.Children.IndexOf(borderFrom) + 1, datetime_);

            messageTemp = msg.id_line;
            dateTimeTemp = dateTime;
            stackPanelTemp = stackPanelFrom;
        }

    }
}
