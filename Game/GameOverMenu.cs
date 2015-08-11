using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SharpPixel
{
    class GameOverMenu : IGameOverMenu
    {
        private enum MenuItem { Retry, Exit }

        private IGame game;
        private IController controller;
        private IRenderSurface surface;

        private Bitmap retryBitmap, exitBitmap, arrowBitmap, scoreBitmap;
        
        private int arrowPos;

        private int score;

        private MenuItem current = MenuItem.Retry;
        private MenuItem Current
        {
            get { return current; }
            set
            {
                if (current != value)
                {
                    current = value;
                    switch (current)
                    {
                        case MenuItem.Retry:
                            arrowPos = 16;
                            break;
                        case MenuItem.Exit:
                            arrowPos = 31;
                            break;
                    }
                }
            }
        }

        #region Члены IGameOverMenu

        public void Initialize(IGame game)
        {
            this.game = game;
        }

        public void SetScore(int score)
        {
            this.score = score;
            Render();
        }

        #endregion

        #region Члены IScene

        public void SetController(IController controller)
        {
            this.controller = controller;
        }

        public void SetRenderSurface(IRenderSurface surface)
        {
            this.surface = surface;
        }

        public void OnKeyDown(KeyEventArgs e)
        {
            //
            Render();
        }

        public void LoadResources()
        {
            scoreBitmap = ResourceManager.GetBitmapResource("score");
            retryBitmap = ResourceManager.GetBitmapResource("Retry");
            arrowBitmap = ResourceManager.GetBitmapResource("arrow");
            exitBitmap = ResourceManager.GetBitmapResource("exit");            
        }

        public void Render()
        {            
            surface.RenderBackground(Utility.GrayMiddle);
            surface.RenderBitmap(scoreBitmap, 15, 5);
            surface.RenderNumber(score, 10, 20, -1);
            surface.RenderBitmap(retryBitmap, 15, 35);
            surface.RenderBitmap(exitBitmap, 15, 50);
            surface.RenderBitmap(arrowBitmap, 6, arrowPos);
            surface.SwapBuffers();
        }

        public void Update(double dt)
        {
            // Nothing
        }

        #endregion
    }
}
