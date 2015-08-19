using System.Drawing;

namespace SharpPixel.Game.GameObjects
{
    public class Fuel : GameObject
    {
        public Fuel(Bitmap bitmap, Point location) : base(bitmap, location)
        {
            this.Type = GameObjectType.Collectable;
        }

        public override void OnCollect(GameObject gameObject)
        {
            base.OnCollect(gameObject);
            (gameObject as Player).FuelLevel += Utility.FUEL_ADD;
        }

        public override void Update(double dt)
        {
            base.Update(dt);
            if (Location.Y > Utility.FIELD_SIZE + 1)
                Active = false;
        }
    }
}
