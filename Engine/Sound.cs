using System;
using System.Collections.Generic;
using Microsoft.DirectX.AudioVideoPlayback;

namespace SharpPixel.Engine
{
    /// <summary>
    /// Simple manager for sound playback
    /// </summary>
    public class Sound : ISound
    {
        /// <summary>
        /// Internal storage for audio objects
        /// </summary>
        private Dictionary<Sounds, Audio> sounds = new Dictionary<Sounds, Audio>();

        /// <summary>
        /// Loads audio resources into memory
        /// </summary>
        public void LoadResources()
        {
            foreach (Sounds en in Enum.GetValues(typeof(Sounds)))
            {
                var sound = new Audio(ResourceManager.GetAudioResourcePath(en.ToString()), false);
                sound.Ending += (o, e) => { sound.Stop(); sound.CurrentPosition = 0; };
                sounds.Add(en, sound);
            }
        }

        /// <summary>
        /// Plays specified sound.
        /// </summary>
        /// <param name="sound">Sound to play</param>
        public void Play(Sounds sound)
        {
            sounds[sound].Stop();
            sounds[sound].Play();
        }
    }
}
