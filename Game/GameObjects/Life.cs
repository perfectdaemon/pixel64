﻿using System.Drawing;

namespace SharpPixel.Game.GameObjects
{
    public class Life : GameObject
    {
        public Life(Bitmap bitmap, Point location)
            : base(bitmap, location)
        {
            this.Type = GameObjectType.Collectable;
        }

        public override void OnCollect(GameObject gameObject)
        {
            (gameObject as Player).OnLifeCollect();
            base.OnCollect(gameObject);
        }
    }
}
