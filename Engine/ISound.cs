using System;
namespace SharpPixel.Engine
{
    public enum Sounds { Hit, LifePickup, Pickup }

    public interface ISound
    {
        void LoadResources();
        void Play(Sounds sound);
    }
}
