using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SharpPixel.Engine;

namespace SharpPixel.Game.GameObjects
{
    public class Player : CarBase
    {
        private double fuelLevel;
        public double FuelLevel
        {
            get { return fuelLevel; }
            set 
            {
                fuelLevel = Utility.Clamp(value, 0, Utility.FUEL_MAX);
            }
        }

        public Player(Bitmap bitmap, Bitmap shadowBitmap, Bitmap smokeBitmap, Point location)
            : base(bitmap, shadowBitmap, smokeBitmap, location)
        {            
            this.Type = GameObjectType.Player;
            this.FuelLevel = Utility.FUEL_MAX;            
        }

        public SimpleAction OnLifeCollect;        

        public override void Update(double dt)
        {
            base.Update(dt);
            if (Utility.KeyDown[Keys.Left] && this.Location.X > 0)            
                this.Location.X -= 2;            
            else if (Utility.KeyDown[Keys.Right] && this.Location.X < Utility.FIELD_SIZE - Bitmap.Width)            
                this.Location.X += 2;            

            if (Utility.KeyDown[Keys.Up] && this.Location.Y > 0)
                this.Location.Y--;
            else if (Utility.KeyDown[Keys.Down] && this.Location.Y < Utility.FIELD_SIZE - Bitmap.Height - 7)
                this.Location.Y++;

            if (FuelLevel > 0)
                FuelLevel -= Utility.FUEL_DEC * dt;
        }
    }
}
