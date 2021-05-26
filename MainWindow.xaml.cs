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
        Color colorBG = new Color();
        Color colorFG = new Color();
        public MainWindow()
        {
            InitializeComponent();
            colorBG = Colors.White;
            colorFG = Colors.Black;
        }

        private void AddNewTab_Click(object sender, RoutedEventArgs e)
        {
            if (TabControl.Items.Count != 17)
            {
                List<TabItem> tabs = new List<TabItem>();
                foreach (TabItem t in TabControl.Items)
                    tabs.Add(t);

                //содержимое вкладки (браузер)
                ChromiumWebBrowser webBros = new ChromiumWebBrowser
                {
                    Address = ("google.com"),
                    Margin = new Thickness(0, 30, 0, 0)
                };

                //контекстное меню вкладки
                ContextMenu contextMenu = new ContextMenu();
                {
                    MenuItem item = new MenuItem();

                    item.Header = "Создать новую вкладку";
                    item.Click += AddNewTab_Click;
                    contextMenu.Items.Add(item);

                    item = new MenuItem();
                    item.Header = "Дублировать";
                    item.Click += duplicate_Click;
                    contextMenu.Items.Add(item);

                    item = new MenuItem();
                    item.Header = "Инкогнито";
                    item.Click += incognito_Click;
                    contextMenu.Items.Add(item);

                    item = new MenuItem();
                    item.Header = "Закрепить";
                    item.Click += anchor_Click;
                    contextMenu.Items.Add(item);

                    item = new MenuItem();
                    item.Header = "Обновить";
                    item.Click += refresh_Click;
                    contextMenu.Items.Add(item);

                    item = new MenuItem();
                    item.Header = "Печать";
                    item.Click += print_Click;
                    contextMenu.Items.Add(item);

                    item = new MenuItem();
                    item.Header = "Закрыть";
                    item.Click += delete_Click;
                    contextMenu.Items.Add(item);
                }

                //новая вкладка
                TabItem newTab = new TabItem
                {
                    Header = "Новая вкладка",
                    Tag = "standart",
                    Background = new SolidColorBrush(colorBG),
                    Foreground = new SolidColorBrush(colorFG),
                    Content = webBros,
                    ContextMenu = contextMenu
                };
                newTab.MouseRightButtonUp += TabItem_MouseRightButtonUp;
                tabs.Add(newTab);

                //удаляем все вкладки и заново их добавляем
                TabControl.Items.Clear();
                foreach (TabItem item in tabs)
                    TabControl.Items.Add(item);
                TabControl.SelectedIndex = (TabControl.Items.Count - 1);
            }
        }
        private void incognito_Click(object sender, RoutedEventArgs e)
        {
            if (TabControl.Items.Count != 17)
            {
                List<TabItem> tabs = new List<TabItem>();
                foreach (TabItem t in TabControl.Items)
                    tabs.Add(t);

                //содержимое вкладки (браузер)
                ChromiumWebBrowser webBros = new ChromiumWebBrowser
                {
                    Address = ("google.com"),
                    Margin = new Thickness(0, 30, 0, 0)
                };

                //контекстное меню вкладки
                ContextMenu contextMenu = new ContextMenu();
                {
                    MenuItem item = new MenuItem();

                    item.Header = "Создать новую вкладку";
                    item.Click += AddNewTab_Click;
                    contextMenu.Items.Add(item);

                    item.Header = "Дублировать";
                    item.Click += duplicate_Click;
                    contextMenu.Items.Add(item);

                    item.Header = "Инкогнито";
                    item.Click += incognito_Click;
                    contextMenu.Items.Add(item);

                    item.Header = "Закрепить";
                    item.Click += anchor_Click;
                    contextMenu.Items.Add(item);

                    item.Header = "Обновить";
                    item.Click += refresh_Click;
                    contextMenu.Items.Add(item);

                    item.Header = "Печать";
                    item.Click += print_Click;
                    contextMenu.Items.Add(item);

                    item.Header = "Закрыть";
                    item.Click += delete_Click;
                    contextMenu.Items.Add(item);
                }

                //новая вкладка
                TabItem newTab = new TabItem
                {
                    Header = "Инкогнито",
                    Tag = "incognito",
                    Background = new SolidColorBrush(colorBG),
                    Foreground = new SolidColorBrush(colorFG),
                    Content = webBros,
                    ContextMenu = contextMenu
                };
                newTab.MouseRightButtonUp += TabItem_MouseRightButtonUp;
                tabs.Add(newTab);

                //удаляем все вкладки и заново их добавляем
                TabControl.Items.Clear();
                foreach (TabItem item in tabs)
                    TabControl.Items.Add(item);
                TabControl.SelectedIndex = (TabControl.Items.Count - 1);
            }
        }
        private void duplicate_Click(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = TabControl.SelectedItem as TabItem;
            int index = tabItem.TabIndex;
            if (TabControl.Items.Count != 17)
            {
                List<TabItem> tabs = new List<TabItem>();
                foreach (TabItem t in TabControl.Items)
                    tabs.Add(t);

                //новая вкладка
                TabItem newTab = new TabItem
                {
                    Header = tabItem.Header,
                    Tag = tabItem.Tag,
                    Background = tabItem.Background,
                    Foreground = tabItem.Foreground,
                    Content = tabItem.Content,
                    ContextMenu = tabItem.ContextMenu
                };
                newTab.MouseRightButtonUp += TabItem_MouseRightButtonUp;

                for (int i = 0; i < tabs.Count; i++)
                    if (i == index)
                        tabs.Add(newTab);

                //удаляем все вкладки и заново их добавляем
                TabControl.Items.Clear();
                foreach (TabItem item in tabs)
                    TabControl.Items.Add(item);
                TabControl.SelectedIndex = (TabControl.Items.Count - 1);
            }
        }
        private void anchor_Click(object sender, RoutedEventArgs e)
        {
            //вкладка, которую мы закрепляем
            TabItem pinnedTab = TabControl.SelectedItem as TabItem;
            TabControl.Items.RemoveAt(TabControl.SelectedIndex);

            List<TabItem> tabs = new List<TabItem>
            {
                pinnedTab
            };
            foreach (TabItem t in TabControl.Items)
                tabs.Add(t);

            //удаляем все вкладки и заново их добавляем
            TabControl.Items.Clear();
            foreach (TabItem item in tabs)
                TabControl.Items.Add(item);
            TabControl.SelectedIndex = 0;
        }
        private void delete_Click(object sender, RoutedEventArgs e)
        {
            TabControl.Items.RemoveAt(TabControl.SelectedIndex);

            if (TabControl.Items.Count == 0)
                Application.Current.Shutdown();
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

        private void search_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if ((TabControl.SelectedItem as TabItem).Tag.ToString() != "history")
                {
                    ChromiumWebBrowser browser = (TabControl.SelectedItem as TabItem).Content as ChromiumWebBrowser;
                    browser.Address = search.Text;

                    if ((TabControl.SelectedItem as TabItem).Tag.ToString() == "standart")
                    {
                        (TabControl.SelectedItem as TabItem).Header = browser.Address;
                        history.Add("\tДата: " + DateTime.Now.ToShortDateString() +
                           "\t| Время: " + DateTime.Now.ToShortTimeString() +
                           "\t|  Сайт: " + browser.Address);
                    }
                }
            }
        }

        private void forward_Click(object sender, RoutedEventArgs e)
        {
            if ((TabControl.SelectedItem as TabItem).Tag.ToString() != "history")
            {
                ChromiumWebBrowser browser = (TabControl.SelectedItem as TabItem).Content as ChromiumWebBrowser;
                browser.Forward();
                (TabControl.SelectedItem as TabItem).Header = browser.Address;
            }
        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            if ((TabControl.SelectedItem as TabItem).Tag.ToString() != "history")
            {
                ChromiumWebBrowser browser = (TabControl.SelectedItem as TabItem).Content as ChromiumWebBrowser;
                browser.Back();
                (TabControl.SelectedItem as TabItem).Header = browser.Address;
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            for (int i = TabControl.Items.Count - 1; i > 0; i--)
                if ((TabControl.Items[i] as TabItem) != TabControl.SelectedItem)
                    TabControl.Items.RemoveAt(i);
        }

        private void history_Click(object sender, RoutedEventArgs e)
        {
            if (TabControl.Items.Count != 17)
            {
                List<TabItem> tabs = new List<TabItem>();
                foreach (TabItem t in TabControl.Items)
                    tabs.Add(t);

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
                    Header = "История",
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
            ((TabControl.SelectedItem as TabItem).Content as ChromiumWebBrowser).Print();
        }

        private void backgroundCP_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            colorBG = (sender as ColorPicker).SelectedColor ?? Colors.Black;
            string s = colorBG.ToString();
            bool _ = int.TryParse(s.Substring(3, 1), out int darkness);

            if (darkness <= 5 && !Color.AreClose(colorBG, Colors.White) &&
                !(s[3] == 'A' || s[3] == 'B' || s[3] == 'C' || s[3] == 'D' || s[3] == 'E' || s[3] == 'F'))
                colorFG = Colors.White;            
            else colorFG = Colors.Black;

            //цвет шрифта
            AddNewTab.Foreground = new SolidColorBrush(colorFG);
            clear.Foreground = new SolidColorBrush(colorFG);
            back.Foreground = new SolidColorBrush(colorFG);
            forward.Foreground = new SolidColorBrush(colorFG);
            refresh.Foreground = new SolidColorBrush(colorFG);
            historyButton.Foreground = new SolidColorBrush(colorFG);
            print.Foreground = new SolidColorBrush(colorFG);
            backgroundTab.Foreground = new SolidColorBrush(colorFG);
            backgroundInterface.Foreground = new SolidColorBrush(colorFG);
            for (int i = 0; i < TabControl.Items.Count; i++)
                (TabControl.Items[i] as TabItem).Foreground = new SolidColorBrush(colorFG);

            //цвет фона
            AddNewTab.Background = new SolidColorBrush(colorBG);
            clear.Background = new SolidColorBrush(colorBG);
            back.Background = new SolidColorBrush(colorBG);
            forward.Background = new SolidColorBrush(colorBG);
            refresh.Background = new SolidColorBrush(colorBG);
            historyButton.Background = new SolidColorBrush(colorBG);
            print.Background = new SolidColorBrush(colorBG);
            backgroundTab.Background = new SolidColorBrush(colorBG);
            backgroundInterface.Background = new SolidColorBrush(colorBG);
            for (int i = 0; i < TabControl.Items.Count; i++)
                (TabControl.Items[i] as TabItem).Background = new SolidColorBrush(colorBG);
        }

        private void TabItem_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            TabControl.SelectedItem = sender as TabItem;
        }
    }
}
