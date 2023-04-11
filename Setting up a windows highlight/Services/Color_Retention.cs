using Microsoft.Win32;
using System;
using System.IO;

namespace Setting_up_a_windows_highlight.Services
{
    class Color_Retention
    {
        const string THE_PATH_SYSTEM_COLORS = "Control Panel\\Colors";

        private string The_Path_The_Recorded_Color { get; } = $"{Environment.CurrentDirectory}\\Resources\\File\\Recorded colors.txt".Replace("\\bin\\Debug\\net6.0-windows", "");

        public void Save(string hot_Tracking_Color)
        {
            hot_Tracking_Color.Replace(',', ' ');
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(THE_PATH_SYSTEM_COLORS, true))
            {
                key.SetValue("HotTrackingColor", hot_Tracking_Color);
                key.SetValue("Hilight", hot_Tracking_Color);
            }
        }

        public void Save(string hot_Tracking_Color,bool save_Color_File)
        {
            if (save_Color_File)
            {
                using (FileStream file = new FileStream(The_Path_The_Recorded_Color, FileMode.Append))
                {
                    StreamWriter writer = new StreamWriter(file);
                    writer.WriteLine(hot_Tracking_Color);
                    writer.Close();
                }
            }
            else
            {
                Save(hot_Tracking_Color);
            }
        }
    }
}
