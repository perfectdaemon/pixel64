﻿using System;
using System.Drawing;
using SharpPixel.Engine;

namespace SharpPixel.Game.GameObjects
{
    /// <summary>
    /// Base object for cars in the game - player and civilian ones
    /// </summary>
    public class CarBase : GameObject
    {
        /// <summary>
        /// Used for smoke animation
        /// </summary>
        protected double smokeRotation = new Random().NextDouble() * 10, smokeRotationDirection = 1.0d;

        /// <summary>
        /// Car chadow bitmap
        /// </summary>
        public Bitmap ShadowBitmap;

        /// <summary>
        /// Car smoke bitmap
        /// </summary>
        public Bitmap SmokeBitmap;        

        /// <summary>
        /// Instantiates object of this class
        /// </summary>
        /// <param name="bitmap">Car bitmap</param>
        /// <param name="shadowBitmap">Car shadow bitmap</param>
        /// <param name="smokeBitmap">Car smoke bitmap</param>
        /// <param name="location">Start location of car</param>
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
            //  Render shadow and car itself
            surface.RenderBitmap(ShadowBitmap, Location.X + 1, Location.Y + 1);                     
            base.Render(surface);                                   

            // Calculate and render smoke
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
