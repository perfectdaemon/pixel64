using System.Drawing;

namespace SharpPixel.Game.GameObjects
{
    public class RoadSign : GameObject
    {        
        public RoadSign(Bitmap bitmap, Point location)
            : base(bitmap, location)
        {
            this.Type = GameObjectType.Obstacle;
            this.CollisionRect = new Rectangle(2, 2, bitmap.Width - 4, bitmap.Height - 2);
        }

        public override void Update(double dt)
        {
            base.Update(dt);
            if (Location.Y > Utility.FIELD_SIZE + 1)
                Active = false;
        }
    }
}
