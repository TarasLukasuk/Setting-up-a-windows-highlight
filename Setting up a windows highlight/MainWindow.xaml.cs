using Microsoft.Win32;
using Setting_up_a_windows_highlight.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Setting_up_a_windows_highlight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private string The_Path_The_Recorded_Color { get; } = $"{Environment.CurrentDirectory}\\Resources\\File\\Recorded colors.txt".Replace("\\bin\\Debug\\net6.0-windows", "");

        private void Move_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => Set_Brder_Brush_Color(RGB_Backgraund);

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e) => Set_Brder_Brush_Color(HEX_Backgraund);

        String_Сonversion_Сolor string_Сonversion_Сolor = new String_Сonversion_Сolor();

        private void Border_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Show_Color.Background= string_Сonversion_Сolor._Convert_String_RGB_And_ARGB_And_HEX_Color(RGB_Text.Text);
                HEX_Text.Text = string_Сonversion_Сolor.Get_HEX_To_String();
            }
        }

        private void Border_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Show_Color.Background = string_Сonversion_Сolor._Convert_String_HEX_And_RGB_Color(HEX_Text.Text);
                RGB_Text.Text = string_Сonversion_Сolor.Get_RGB_To_String();
            }
        }

        private void Set_Brder_Brush_Color(Border border)
        {
            Brush myBrush = new SolidColorBrush(Colors.Blue);
            border.Background = myBrush;
        }

        private void Save_Color_Click(object sender, RoutedEventArgs e)
        {
            Color_Retention color_Retention = new Color_Retention();
            color_Retention.Save(RGB_Text.Text);
            color_Retention.Save(string_Сonversion_Сolor.Get_HEX_To_String(), true);

            timer.Tick += new EventHandler(Timer_Tick_Restart_PC);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private DispatcherTimer timer = new DispatcherTimer();

        private void Reset_Color_Click(object sender, RoutedEventArgs e)
        {
            const string THE_PATH_SYSTEM_COLORS = "Control Panel\\Colors";
            const string HOT_TRACKING_COLOR = "0 102 204";
            const string HILIGHT = "0 120 215";

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(THE_PATH_SYSTEM_COLORS, true))
            {
                key.SetValue("HotTrackingColor", HOT_TRACKING_COLOR);
                key.SetValue("Hilight", HILIGHT);
            }

           timer.Tick += new EventHandler(Timer_Tick_Reset);
           timer.Interval = new TimeSpan(0, 0, 1);
           timer.Start();
        }

        private int seconds = 5;

        private void Timer_Tick_Reset(object? sender, EventArgs e)
        {
            if(seconds==0)
            {
                timer.Stop();
                timer.Tick -= new EventHandler(Timer_Tick_Reset);
                Process.Start("shutdown", "/r /t 0");
            }

            Warning.Text = $"Увага ваш комп'ютер буде перезавантажено через {seconds--} секунд";
        }

        private void Old_Selected_Colors_Loaded(object sender, RoutedEventArgs e)
        {
            using (FileStream file = new FileStream(The_Path_The_Recorded_Color, FileMode.Open))
            {
                StreamReader reader = new StreamReader(file);

                string result_Reader = reader.ReadToEnd();
                Old_Selected_Colors.Document.Blocks.Clear();
                Old_Selected_Colors.Document.Blocks.Add(new Paragraph(new Run(result_Reader + Environment.NewLine)));
                reader.Close();
            }
        }

        private void Timer_Tick_Restart_PC(object? sender, EventArgs e)
        {
            if (seconds <= 0)
            {
                timer.Stop();
                timer.Tick -= new EventHandler(Timer_Tick_Reset);
                Process.Start("shutdown", "/r /t 0");
            }

            Warning.Text = $"Увага ваш комп'ютер буде перезавантажено через {seconds--} секунд";
        }

        private void Exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => Close();

        private void Exit_MouseEnter(object sender, MouseEventArgs e) => Exit.Foreground = new SolidColorBrush(Colors.Red);
    }
}
