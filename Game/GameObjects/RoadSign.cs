﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SharpPixel.Game.GameObjects
{
    public class RoadSign : GameObject
    {        
        public RoadSign(Bitmap bitmap, Point location)
            : base(bitmap, location)
        {
            this.Type = GameObjectType.Obstacle;
        }

        public override void Update(double dt)
        {
            base.Update(dt);
            if (Location.Y > Utility.FIELD_SIZE + 1)
                Active = false;
        }
    }
}
