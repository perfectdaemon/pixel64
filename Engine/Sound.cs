using System;
using System.Collections.Generic;
using System.Media;
using System.Text;
using Microsoft.DirectX.AudioVideoPlayback;

namespace SharpPixel.Engine
{    
    public class Sound : ISound
    {
        private Dictionary<Sounds, Audio> sounds = new Dictionary<Sounds, Audio>();        
        
        public void LoadResources()
        {

            foreach (Sounds en in Enum.GetValues(typeof(Sounds)))
            {
                var sound = new Audio(ResourceManager.GetAudioResourcePath(en.ToString()), false);                
                sound.Ending += (o, e) => { sound.Stop(); sound.CurrentPosition = 0; };
                sounds.Add(en, sound);
            }            
        }

        public void Play(Sounds sound)
        {
            sounds[sound].Stop();
            sounds[sound].Play();
        }      
    }
}
