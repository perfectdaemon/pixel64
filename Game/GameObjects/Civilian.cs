using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SharpPixel.Game.GameObjects
{
    public class Civilian : CarBase
    {
        private double distanceD;
        private int distance;
        public int Speed = 1 + new Random().Next(3);        
        public Civilian(Bitmap bitmap, Bitmap shadowBitmap, Bitmap smokeBitmap, Point location)
            : base(bitmap, shadowBitmap, smokeBitmap, location)
        {
            this.Type = GameObjectType.Civilian;
        }

        public override void Update(double dt)
        {
            base.Update(dt);
            
            int oldDistance = distance;
            distanceD += Speed * dt;
            distance = (int)Math.Floor(distanceD);

            this.Location.Y += (oldDistance - distance);

            if (Location.Y > Utility.FIELD_SIZE + 1)
                Active = false;
        }
    }
}
