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

        private int distance = 0;
        private double smokeRotation = 0.0d, smokeRotationDirection = 1.0d, carRotation = 0.0d;
        private double distanceD = 0.0d;

        private Point carPos;
        private Bitmap carBitmap, carShadowBitmap, fuelBitmap, roadSignBitmap, smokeBitmap;

        private Pen pen = new Pen(Utility.Yellow, 1);

        private void RenderRoadMarking()
        {
            pen.Color = Utility.Yellow;
            int yOffset = distance % Utility.FIELD_SIZE - Utility.FIELD_SIZE;
            int xOffset = Utility.FIELD_SIZE / Utility.LANES_COUNT + 1;
            for (int i = 0; i < Utility.LANES_COUNT - 1; ++i)
            {
                for (int j = 0; j < 16; ++j)
                    surface.BackGraphics.DrawLine(pen, xOffset * (i + 1), yOffset + j * 8, xOffset * (i + 1), yOffset + j * 8 + 6);
            }

            //borders
            pen.Color = Utility.GrayHard;
            surface.BackGraphics.DrawLine(pen, 0, 0, 0, Utility.FIELD_SIZE);
            surface.BackGraphics.DrawLine(pen, Utility.FIELD_SIZE - 1, 0, Utility.FIELD_SIZE - 1, Utility.FIELD_SIZE);
        }

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
            //Render();
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
            this.RenderRoadMarking();
            surface.RenderBitmap(carShadowBitmap, carPos.X + 1, carPos.Y + 1);
            surface.RenderBitmap(carBitmap, carPos);
            surface.RenderBitmap(smokeBitmap, carPos.X, carPos.Y + 16, (int) smokeRotation);
            surface.RenderNumber(distance, 2, Utility.FIELD_SIZE - 10, -1);
            surface.SwapBuffers();
        }

        public void Update(double dt)
        {            
            if (Utility.KeyDown[Keys.Left])
            {
                carPos.X--;                
            }
            else if (Utility.KeyDown[Keys.Right])
            {
                carPos.X++;                
            }

            if (Utility.KeyDown[Keys.Up])
                carPos.Y--;
            else if (Utility.KeyDown[Keys.Down])
                carPos.Y++;
            Render();
                        
            if (smokeRotation > 15 || smokeRotation < -15)
                smokeRotationDirection = -smokeRotationDirection;
            smokeRotation += 60 * dt * smokeRotationDirection;

            distanceD += dt * 20;
            distance = (int)Math.Floor(distanceD);
        }
    }
}
