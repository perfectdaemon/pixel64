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
        private IMainMenu mainMenu;
        private IGameOverMenu gameOverMenu;
        private IController controller;

        private Point carPos;
        private Bitmap carBitmap, carShadowBitmap, fuelBitmap, roadSignBitmap, smokeBitmap;
        
        public void SetController(IController controller)
        {
            this.controller = controller;
        }

        public void SetRenderSurface(IRenderSurface surface)
        {
            this.surface = surface;
        }

        public void Initialize(IMainMenu mainMenu, IGameOverMenu gameOverMenu)
        {            
            this.mainMenu = mainMenu;
            this.gameOverMenu = gameOverMenu;
            carPos = new Point(10, 10);
        }

        public void OnKeyDown(KeyEventArgs e)
        {            
            Render();
        }

        public void LoadResources()
        {
            carBitmap = ResourceManager.GetBitmapResource("Car");
            carShadowBitmap = ResourceManager.GetBitmapResource("CarShadow");
            fuelBitmap = ResourceManager.GetBitmapResource("Fuel");
            roadSignBitmap = ResourceManager.GetBitmapResource("Roadsign");
            smokeBitmap = ResourceManager.GetBitmapResource("smoke");
        }

        public void Render()
        {
            surface.RenderBackground(Utility.GrayLight);
            surface.RenderBitmap(carShadowBitmap, carPos.X + 1, carPos.Y + 1);
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
            Render();

            gameOverMenu.SetScore(55023);            
        }
    }
}
