﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SharpPixel.Game.GameObjects
{
    public class GameObject
    {
        public bool Active = true;
        public bool Visible = true;
        public Bitmap Bitmap;
        public Rectangle CollisionRect;
        public Point Location;        

        public GameObjectType Type { get; protected set; }

        public GameObject(Bitmap bitmap, Point location)
        {
            this.Bitmap = bitmap;
            this.Location = location;
            if (bitmap != null)
                this.CollisionRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            else
                this.CollisionRect = new Rectangle();
            
            this.Type = GameObjectType.Trigger;
        }

        public Rectangle GetAbsoluteCollisonRect()
        { 
            Rectangle absoluteCollisionRect = CollisionRect;            
            absoluteCollisionRect.Offset(Location);
            return absoluteCollisionRect;
        }

        public bool DoesCollideWith(GameObject gameObject)
        {
            return GetAbsoluteCollisonRect().IntersectsWith(gameObject.GetAbsoluteCollisonRect());
        }    
    
        public virtual void Update(double dt)
        {

        }

        public virtual void Render(IRenderSurface surface)
        {
            if (Visible)
                surface.RenderBitmap(Bitmap, Location);
        }

        public virtual void OnCollect(GameObject gameObject)
        {
            Active = false;
        }

        public virtual void OnTrigger()
        { 

        }
    }
}