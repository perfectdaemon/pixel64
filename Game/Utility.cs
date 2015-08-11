using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SharpPixel.Properties;

namespace SharpPixel
{
    static class Utility
    {
        public const int FIELD_SIZE = 64;        
        
        public static readonly Size WindowSize;

        public static readonly Color GrayLight = Color.FromArgb(122, 122, 122);
        public static readonly Color GrayMiddle = Color.FromArgb(98, 98, 98);

        public static readonly Dictionary<Keys, bool> KeyDown = new Dictionary<Keys, bool>();

        static Utility()
        { 
            var settings = Settings.Default;
            WindowSize = new Size(settings.WindowSize, settings.WindowSize);

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                if (!KeyDown.ContainsKey(key))
                    KeyDown.Add(key, false);
        }        
    }
}
