using System;
using System.Collections.Generic;
using System.Drawing;

namespace SharpPixel.Engine
{
    class ResourceManager
    {
        private static readonly Dictionary<string, Bitmap> bitmapResources = new Dictionary<string, Bitmap>();

        private static string GetResourcePath(string resourceName)
        {
            return string.Format("sprites/{0}.png", resourceName);
        }

        public static Bitmap GetBitmapResource(string resourceName, bool clone = false)
        {
            try
            {
                if (clone)
                    return new Bitmap(GetBitmapResource(resourceName));

                if (!bitmapResources.ContainsKey(resourceName))
                    bitmapResources.Add(resourceName, new Bitmap(GetResourcePath(resourceName)));

                return bitmapResources[resourceName];
            }
            catch (ArgumentException aex)
            {
                throw new Exception(
                    string.Format("Не удалось загрузить ресурс `{0}`, используя путь `{1}`",
                        resourceName,
                        GetResourcePath(resourceName)),
                    aex);
            }
        }

        public static string GetAudioResourcePath(string resourceName)
        {
            return string.Format("sounds/{0}.ogg", resourceName);
        }
    }
}
