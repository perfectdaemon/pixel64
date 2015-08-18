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

        private IGameScene game;
        private IController controller;
        private IRenderSurface surface;

        private Bitmap retryBitmap, exitBitmap, arrowBitmap, scoreBitmap;
        
        private int arrowPos = 36;

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
                            arrowPos = 36;
                            break;
                        case MenuItem.Exit:
                            arrowPos = 52;
                            break;
                    }
                }
            }
        }

        #region Члены IGameOverMenu

        public void Initialize(IGameScene game)
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
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                    Current = (Current == MenuItem.Retry) ? MenuItem.Exit : MenuItem.Retry;
                    Render();
                    break;

                case Keys.Return:
                    switch (Current)
                    {
                        case MenuItem.Retry:
                            game.Reset();
                            controller.SwitchTo(game);
                            break;
                        case MenuItem.Exit:
                            Application.Exit();
                            break;
                    }
                    break;

                case Keys.Escape:
                    Application.Exit();
                    break;
                default:
                    break;
            } 
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

            int xOffset = score.ToString().Length * 3 / 2;
            surface.RenderNumber(score, 30 - xOffset, 20, -1);

            surface.RenderBitmap(retryBitmap, 15, 35);
            surface.RenderBitmap(exitBitmap, 15, 50);
            surface.RenderBitmap(arrowBitmap, 6, arrowPos);
            surface.SwapBuffers();
        }

        public void Update(double dt)
        {
            // Nothing
        }

        public void Reset()
        {
            score = 0;
        }

        #endregion
    }
}
