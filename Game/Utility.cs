using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SharpPixel.Properties;

namespace SharpPixel
{
    public static class Utility
    {
         // Consts block
        public const int FIELD_SIZE = 64;
        public const int LANES_COUNT = 4;

        public const double SPEED_START = 23.0d,
            SPEED_INC = 5d,
            SPEED_MAX = 75d;

        public const double FUEL_MAX = 100.0d,
            FUEL_DEC = 5.0d,
            FUEL_ADD = 25.0d;

        public const double SPAWN_PERIOD = 1.4d,
            SPAWN_FUEL_PERIOD = 4.0d;

        public const int LIVES_START = 3,
            LIVES_MAX = 5;
        
        public static readonly Size WindowSize;

        public static int LaneWidth { get { return FIELD_SIZE / LANES_COUNT; } }

        // Colors block

        public static readonly Color GrayLight = Color.FromArgb(122, 122, 122);
        public static readonly Color GrayMiddle = Color.FromArgb(98, 98, 98);
        public static readonly Color GrayHard = Color.FromArgb(73, 73, 73);
        public static readonly Color Yellow = Color.FromArgb(243, 240, 8);
        public static readonly Color RedLight = Color.FromArgb(244, 74, 110);
        public static readonly Color RedHard = Color.FromArgb(157, 48, 71);

        public static readonly Dictionary<Keys, bool> KeyDown = new Dictionary<Keys, bool>();

        static Utility()
        { 
            var settings = Settings.Default;
            if (settings != null)
                WindowSize = new Size(settings.WindowSize, settings.WindowSize);
            else
                WindowSize = new Size(256, 256);

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                if (!KeyDown.ContainsKey(key))
                    KeyDown.Add(key, false);
        }

        public static double Clamp(double value, double min, double max)
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }

        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }
    }
}
