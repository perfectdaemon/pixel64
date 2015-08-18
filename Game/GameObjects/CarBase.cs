using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SharpPixel.Game.GameObjects
{
    public class CarBase : GameObject
    {
        protected double smokeRotation = new Random().NextDouble() * 10, smokeRotationDirection = 1.0d;

        public Bitmap ShadowBitmap, SmokeBitmap;        

        public CarBase(
            Bitmap bitmap, 
            Bitmap shadowBitmap, 
            Bitmap smokeBitmap,
            Point location)
            : base(bitmap, location)
        {
            this.ShadowBitmap = shadowBitmap;
            this.SmokeBitmap = smokeBitmap;            
        }

        public override void Update(double dt)
        {
            base.Update(dt);            

            if (smokeRotation > 15 || smokeRotation < -15)
                smokeRotationDirection = -smokeRotationDirection;
            smokeRotation += 120 * dt * smokeRotationDirection;
        }

        public override void Render(IRenderSurface surface)
        {
            if (!Visible)
                return;
            surface.RenderBitmap(ShadowBitmap, Location.X + 1, Location.Y + 1);         
            base.Render(surface);            
            
            //surface.RenderBitmap(SmokeBitmap, Location.X, Location.Y + 16, (int)smokeRotation);

            int smokeScale = 0;
            if (smokeRotation > 5)
                smokeScale = 1;
            else if (smokeRotation < -5)
                smokeScale = 0;
            
            surface.BackGraphics.TranslateTransform(Location.X, Location.Y + 16);
            surface.BackGraphics.RotateTransform((float)smokeRotation);
            surface.BackGraphics.DrawImage(SmokeBitmap,
                new Rectangle(0, 0,
                    SmokeBitmap.Width + smokeScale, SmokeBitmap.Height + smokeScale));
            surface.BackGraphics.ResetTransform();
        }
    }
}
