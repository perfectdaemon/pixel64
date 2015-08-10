using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SharpPixel.Properties;

namespace SharpPixel
{
    public static class Utility
    {
        public const int FIELD_SIZE = 64;        
        
        public static readonly Size WindowSize;

        public static readonly Color GrayLight = Color.FromArgb(122, 122, 122);

        public static readonly Dictionary<Keys, bool> KeyDown = new Dictionary<Keys, bool>();

        public static string GetResourcePath(string resourceName)
        {
            return string.Format("sprites/{0}.png", resourceName);
        }

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
