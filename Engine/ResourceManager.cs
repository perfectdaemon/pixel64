using System;
using System.Collections.Generic;
using System.Drawing;

namespace SharpPixel.Engine
{
    /// <summary>
    /// Simple resource manager
    /// </summary>
    public static class ResourceManager
    {
        /// <summary>
        /// Dictionary with "resource name - Bitmap object" pair
        /// </summary>
        private static readonly Dictionary<string, Bitmap> bitmapResources = new Dictionary<string, Bitmap>();

        /// <summary>
        /// Returns full path based on resource name
        /// </summary>
        /// <param name="resourceName">Resource name without extension</param>
        /// <returns>Path to specified resource</returns>
        private static string GetResourcePath(string resourceName)
        {
            return string.Format("sprites/{0}.png", resourceName);
        }

        /// <summary>
        /// Returns bitmap resource based on it's name from internal storage
        /// </summary>
        /// <param name="resourceName">Resource name without extension</param>
        /// <param name="clone">If true - clones resource</param>
        /// <returns>Bitmap resource</returns>
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
                Log.Instance.Write(string.Format("Не удалось загрузить ресурс `{0}`, используя путь `{1}`. {2}",
                        resourceName,
                        GetResourcePath(resourceName), aex));

                throw new Exception(
                    string.Format("Не удалось загрузить ресурс `{0}`, используя путь `{1}`",
                        resourceName,
                        GetResourcePath(resourceName)),
                    aex);
            }
        }

        /// <summary>
        /// Returns full path to audio resources based on resource name
        /// </summary>
        /// <param name="resourceName">Audio file name without extension</param>
        /// <returns>Path to audio resource</returns>
        public static string GetAudioResourcePath(string resourceName)
        {
            return string.Format("sounds/{0}.mp3", resourceName);
        }
    }
}
