using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SharpPixel
{
    class Game : IGame
    {
        private IRenderSurface surface;
        private IGameMenu menu;
        private IController controller;

        private Point carPos;
        private Bitmap carBitmap, carShadowBitmap, fuelBitmap, roadSignBitmap;
        
        public void SetController(IController controller)
        {
            this.controller = controller;
        }

        public void Initialize(IRenderSurface surface, IGameMenu menu)
        {
            this.surface = surface;
            this.menu = menu;

            carPos = new Point(10, 10);
        }

        public void HandleKeys(KeyEventArgs e)
        {            
            RenderSelf();
        }

        public void LoadResources()
        {
            carBitmap = new Bitmap(Utility.GetResourcePath("Car"));
            carShadowBitmap = new Bitmap(Utility.GetResourcePath("CarShadow"));
            fuelBitmap = new Bitmap(Utility.GetResourcePath("Fuel"));
            roadSignBitmap = new Bitmap(Utility.GetResourcePath("Roadsign"));
        }

        public void RenderSelf()
        {
            surface.RenderBackground(Utility.GrayLight);
            surface.RenderBitmap(carShadowBitmap, carPos.X - 1, carPos.Y - 1);
            surface.RenderBitmap(carBitmap, carPos);
            surface.SwapBuffers();
        }

        public void Update(double dt)
        {
            if (Utility.KeyDown[Keys.Left])
                carPos.X--;
            else if (Utility.KeyDown[Keys.Right])
                carPos.X++;

            if (Utility.KeyDown[Keys.Up])
                carPos.Y--;
            else if (Utility.KeyDown[Keys.Down])
                carPos.Y++;
            RenderSelf();
        }
    }
}
