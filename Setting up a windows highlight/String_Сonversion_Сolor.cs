using System;
using System.Windows.Media;

namespace Setting_up_a_windows_highlight
{
    internal class String_Сonversion_Сolor
    {
        public String_Сonversion_Сolor()
        {
            color_Array[0] = string.Empty;
            color_Array[1] = string.Empty;
            color_Array[2] = string.Empty;
        }

        string[] color_Array = new string[3];

        private byte R { get; set; }
        private byte G { get; set; }
        private byte B { get; set; }

        private Brush Convert_String_RGB_And_HEX_Color(string line)
        {
            if (line == null || line == string.Empty)
            {
                throw new ArgumentNullException("The argument is null", nameof(line));
            }

            color_Array = line.Split(',');

            if (color_Array[0] == string.Empty || color_Array[1] == string.Empty || color_Array[2] == string.Empty)
            {
                throw new ArgumentNullException("One of the arguments is not correct, please check again");
            }

            R = byte.Parse(color_Array[0]);
            G = byte.Parse(color_Array[1]);
            B = byte.Parse(color_Array[2]);

            var color_RGB = Color.FromRgb(R, G, B);
            var result = new SolidColorBrush(color_RGB);
            return result;
        }

        private Brush Convert_String_HEX_And_RGB_Color(string line)
        {
            if (line == null || line == string.Empty)
            {
                throw new ArgumentNullException("The argument is null", nameof(line));
            }

            var color = System.Drawing.ColorTranslator.FromHtml(line);
            R = color.R; G = color.G; B = color.B;

            var result = new SolidColorBrush(Color.FromRgb(R, G, B));
            return result;

        }

        public virtual string Get_RGB_To_String() => string.Format("{0},{1},{2}", R, G, B);

        public virtual string Get_HEX_To_String()
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}",R,G,B) + Environment.NewLine;
        }

        public Brush _Convert_String_RGB_And_ARGB_And_HEX_Color(string line) => Convert_String_RGB_And_HEX_Color(line);

        public Brush _Convert_String_HEX_And_RGB_Color(string line) => Convert_String_HEX_And_RGB_Color(line);
    }
}
