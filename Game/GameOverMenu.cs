using System.Drawing;
using System.Windows.Forms;
using SharpPixel.Engine;
using SharpPixel.Game.Interfaces;

namespace SharpPixel.Game
{
    public class GameOverMenu : Scene, IGameOverMenu
    {
        private enum MenuItem { Retry, Exit }

        private IGameScene game;        

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

        public void Initialize(IGameScene game)
        {
            this.game = game;
        }

        public void SetScore(int score)
        {
            this.score = score;
            Render();
        }


        public override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                    Current = (Current == MenuItem.Retry) ? MenuItem.Exit : MenuItem.Retry;
                    sound.Play(Sounds.Pickup);
                    Render();
                    break;

                case Keys.Return:
                    sound.Play(Sounds.LifePickup);
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

        public override void LoadResources()
        {
            scoreBitmap = ResourceManager.GetBitmapResource("score");
            retryBitmap = ResourceManager.GetBitmapResource("Retry");
            arrowBitmap = ResourceManager.GetBitmapResource("arrow");
            exitBitmap = ResourceManager.GetBitmapResource("exit");            
        }

        public override void Render()
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

        public override void Reset()
        {
            score = 0;
        }        
    }
}
