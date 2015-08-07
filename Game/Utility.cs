using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SharpPixel.Properties;

namespace SharpPixel
{
    public static class Utility
    {
        public const int FIELD_SIZE = 64;        
        public static readonly Size WindowSize;

        public static string GetResourcePath(string resourceName)
        {
            return string.Format("sprites/{0}.png", resourceName);
        }

        static Utility()
        { 
            var settings = Settings.Default;
            WindowSize = new Size(settings.WindowSize, settings.WindowSize);
        }
    }
}
