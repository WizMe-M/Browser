using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using CefSharp.Wpf;
using CefSharp;
using Xceed.Wpf;
using Xceed;
using Xceed.Wpf.Toolkit;
using System.Windows.Media;
using System;

namespace Browser
{
    public partial class MainWindow : Window
    {
        List<string> history = new List<string>();
        Color colorTheme = new Color();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddNewTab_Click(object sender, RoutedEventArgs e)
        {
            if (TabControl.Items.Count != 13)
            {
                List<TabItem> tabs = new List<TabItem>();
                foreach (TabItem t in TabControl.Items)
                    tabs.Add(t);

                //название вкладки
                TextBlock tabName = new TextBlock
                {
                    Text = "Новая вкладка",
                    Margin = new Thickness(3)
                };

                //кнопка удалить вкладку
                Button closeTabButton = new Button
                {
                    Content = "X",
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    FontSize = 10,
                    Width = 20,
                    Height = 20
                };
                closeTabButton.Click += Delete_Click;

                //содержимое заголовка вкладки
                StackPanel header = new StackPanel();
                header.Orientation = Orientation.Horizontal;
                header.Children.Add(tabName);
                header.Children.Add(closeTabButton);

                //содержимое вкладки (браузер)
                ChromiumWebBrowser webBros = new ChromiumWebBrowser
                {
                    Address = ("google.com"),
                    Margin = new Thickness(0, 30, 0, 0)
                };

                //новая вкладка
                TabItem newTab = new TabItem
                {
                    Header = header,
                    Tag = "standart",
                    Content = webBros
                };
                tabs.Add(newTab);



                //удаляем все вкладки и заново их добавляем
                TabControl.Items.Clear();
                foreach (TabItem item in tabs)
                    TabControl.Items.Add(item);
                TabControl.SelectedIndex = (TabControl.Items.Count - 1);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            TabControl.Items.RemoveAt(TabControl.SelectedIndex);
            if (TabControl.Items.Count == 0)
                Application.Current.Shutdown();
        }

        private void search_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if ((TabControl.SelectedItem as TabItem).Tag.ToString() != "history")
                {
                    ChromiumWebBrowser browser = (TabControl.SelectedItem as TabItem).Content as ChromiumWebBrowser;
                    browser.Address = search.Text;
                    if ((TabControl.SelectedItem as TabItem).Tag.ToString() == "standart")
                        history.Add("\tДата: " + DateTime.Now.ToShortDateString() +
                            "\t| Время: " + DateTime.Now.ToShortTimeString() +
                            "\t|  Сайт: " + browser.Address);
                }
            }
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            if ((TabControl.SelectedItem as TabItem).Tag.ToString() != "history")
            {
                ChromiumWebBrowser browser = (TabControl.SelectedItem as TabItem).Content as ChromiumWebBrowser;
                browser.Reload();
                search.Text = browser.Address;
                if ((TabControl.SelectedItem as TabItem).Tag.ToString() == "standart")
                    history.Add("\tДата: " + DateTime.Now.ToShortDateString() +
                        "\t| Время: " + DateTime.Now.ToShortTimeString() +
                        "\t|  Сайт: " + browser.Address);
            }
        }

        private void forward_Click(object sender, RoutedEventArgs e)
        {
            if ((TabControl.SelectedItem as TabItem).Tag.ToString() != "history")
            {
                ChromiumWebBrowser browser = (TabControl.SelectedItem as TabItem).Content as ChromiumWebBrowser;
                browser.Forward();
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if ((TabControl.SelectedItem as TabItem).Tag.ToString() != "history" && (TabControl.SelectedItem as TabItem).Tag.ToString() != "incognito")
            {
                ChromiumWebBrowser browser = (TabControl.SelectedItem as TabItem).Content as ChromiumWebBrowser;
                browser.Back();
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            for (int i = TabControl.Items.Count - 1; i >= 0; i--)
                if ((TabControl.Items[i] as TabItem) != TabControl.SelectedItem)
                    TabControl.Items.RemoveAt(i);
        }

        private void backgroundCP_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Color color = (sender as ColorPicker).SelectedColor ?? Colors.Black;
            string s = color.ToString();
            bool _ = int.TryParse(s.Substring(3, 1), out int darkness);

            if (darkness <= 5 && !Color.AreClose(color, Colors.White) &&
                !(s[3] == 'A' || s[3] == 'B' || s[3] == 'C' || s[3] == 'D' || s[3] == 'E' || s[3] == 'F'))
                s = "White";
            else s = "Black";
            
            AddNewTab.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(s);
            clear.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(s);
            back.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(s);
            forward.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(s);
            refresh.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(s);
            historyButton.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(s);
            print.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(s);
            backgroundTab.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(s);
            backgroundInterface.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(s);
            for (int i = 0; i < TabControl.Items.Count - 1; i++)
                (((TabControl.Items[i] as TabItem).
                    Header as StackPanel).
                    Children[1] as Button).
                    Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(s);

            AddNewTab.Background = new SolidColorBrush(color);
            clear.Background = new SolidColorBrush(color);
            back.Background = new SolidColorBrush(color);
            forward.Background = new SolidColorBrush(color);
            refresh.Background = new SolidColorBrush(color);
            historyButton.Background = new SolidColorBrush(color);
            print.Background = new SolidColorBrush(color);
            backgroundTab.Background = new SolidColorBrush(color);
            backgroundInterface.Background = new SolidColorBrush(color);
            for (int i = 0; i < TabControl.Items.Count - 1; i++)
                (((TabControl.Items[i] as TabItem).
                    Header as StackPanel).
                    Children[1] as Button).
                    Background = new SolidColorBrush(color);
        }

        private void AddNewTab_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TabControl.Items.Count != 13)
            {
                List<TabItem> tabs = new List<TabItem>();
                foreach (TabItem t in TabControl.Items)
                    tabs.Add(t);

                //название вкладки
                TextBlock tabName = new TextBlock
                {
                    Text = "Новая вкладка",
                    Margin = new Thickness(3)
                };

                //кнопка удалить вкладку
                Button closeTabButton = new Button
                {
                    Content = "X",
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    FontSize = 10,
                    Width = 20,
                    Height = 20
                };
                closeTabButton.Click += Delete_Click;

                //содержимое заголовка вкладки
                StackPanel header = new StackPanel();
                header.Orientation = Orientation.Horizontal;
                header.Children.Add(tabName);
                header.Children.Add(closeTabButton);

                //содержимое вкладки (браузер)
                ChromiumWebBrowser webBros = new ChromiumWebBrowser
                {
                    Address = ("google.com"),
                    Margin = new Thickness(0, 30, 0, 0)
                };

                //новая вкладка
                TabItem newTab = new TabItem
                {
                    Header = header,
                    Tag = "incognito",
                    Content = webBros
                };
                tabs.Add(newTab);



                //удаляем все вкладки и заново их добавляем
                TabControl.Items.Clear();
                foreach (TabItem item in tabs)
                    TabControl.Items.Add(item);
                TabControl.SelectedIndex = (TabControl.Items.Count - 1);
            }

        }

        private void history_Click(object sender, RoutedEventArgs e)
        {
            if (TabControl.Items.Count != 13)
            {
                List<TabItem> tabs = new List<TabItem>();
                foreach (TabItem t in TabControl.Items)
                    tabs.Add(t);


                //название вкладки
                TextBlock tabName = new TextBlock
                {
                    Text = "История",
                    Margin = new Thickness(3)
                };

                //кнопка удалить вкладку
                Button closeTabButton = new Button
                {
                    Content = "X",
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    FontSize = 10,
                    Width = 20,
                    Height = 20
                };
                closeTabButton.Click += Delete_Click;

                //содержимое заголовка вкладки
                StackPanel header = new StackPanel();
                header.Orientation = Orientation.Horizontal;
                header.Children.Add(tabName);
                header.Children.Add(closeTabButton);

                //содержимое вкладки
                Button b1 = new Button
                {
                    Content = "Удалить",
                    Margin = new Thickness(20, 10, 10, 10),
                    Width = 200
                };
                b1.Click += deleteHistory_Click;
                Button b2 = new Button
                {
                    Content = "Очистить историю",
                    Margin = new Thickness(10, 10, 20, 10),
                    Width = 200
                };
                b2.Click += clearHistory_Click;
                TextBlock text = new TextBlock
                {
                    Width = 500,
                    FontSize = 30,
                    Text = "История:"
                };
                StackPanel banner = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Height = 50
                };
                banner.Children.Add(b1);
                banner.Children.Add(b2);
                banner.Children.Add(text);

                ListBox historyLB = new ListBox
                {
                    MinHeight = 100
                };
                foreach (string s in history)
                    historyLB.Items.Add(s);
                StackPanel panel = new StackPanel
                {
                    Margin = new Thickness(0, 30, 0, 0),
                    Orientation = Orientation.Vertical
                };
                panel.Children.Add(banner);
                panel.Children.Add(historyLB);

                //новая вкладка
                TabItem newTab = new TabItem
                {
                    Header = header,
                    Tag = "history",
                    Content = panel
                };
                tabs.Add(newTab);




                //удаляем все вкладки и заново их добавляем
                TabControl.Items.Clear();
                foreach (TabItem item in tabs)
                    TabControl.Items.Add(item);
                TabControl.SelectedIndex = (TabControl.Items.Count - 1);
            }
        }

        private void deleteHistory_Click(object sender, RoutedEventArgs e)
        {
            ListBox lbox = (((TabControl.SelectedItem as TabItem).Content as StackPanel).Children[1] as ListBox);
            if (lbox.SelectedItem != null)
            {
                history.RemoveAt(lbox.SelectedIndex);
                lbox.Items.RemoveAt(lbox.SelectedIndex);
            }
        }

        private void clearHistory_Click(object sender, RoutedEventArgs e)
        {
            ListBox lbox = (((TabControl.SelectedItem as TabItem).Content as StackPanel).Children[1] as ListBox);
            for (int i = lbox.Items.Count - 1; i >= 0; i--)
            {
                lbox.Items.RemoveAt(i);
                history.RemoveAt(i);
            }
        }

        private void print_Click(object sender, RoutedEventArgs e)
        {
            ChromiumWebBrowser browser = (TabControl.SelectedItem as TabItem).Content as ChromiumWebBrowser;
            browser.Print();
        }
    }
}
