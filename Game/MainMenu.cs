using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SharpPixel
{
    class MainMenu : IMainMenu
    {
        private enum MenuItem { Game, Exit }

        private IRenderSurface surface;
        private IController controller;
        private IGame game;

        private Bitmap gameBitmap, exitBitmap, forIgdcBitmap, arrowBitmap, paletteBitmap;        
        private int arrowPos;
        
        private MenuItem current = MenuItem.Game;
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
                        case MenuItem.Game:
                            arrowPos = 16;
                            break;
                        case MenuItem.Exit:
                            arrowPos = 31;
                            break;
                    }
                }
            }
        }

        public void SetController(IController controller)
        {
            this.controller = controller;
        }

        public void SetRenderSurface(IRenderSurface surface)
        {
            this.surface = surface;
        }

        public void Initialize(IGame game)
        {            
            this.game = game;
        }

        public void LoadResources()
        {
            try
            {
                gameBitmap = ResourceManager.GetBitmapResource("game"); //new Bitmap(Utility.GetResourcePath("game"));
                exitBitmap = ResourceManager.GetBitmapResource("exit");
                forIgdcBitmap = ResourceManager.GetBitmapResource("igdc");
                arrowBitmap = ResourceManager.GetBitmapResource("arrow");
                paletteBitmap = ResourceManager.GetBitmapResource("palette");
                arrowPos = 16;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке ресурсов меню: " + ex.ToString());
            }
        }

        public void Render()
        {            
            surface.RenderBackground(Utility.GrayMiddle);
            //surface.RenderBitmap(paletteBitmap, 1, 1);
            surface.RenderBitmap(gameBitmap, 15, 15);
            surface.RenderBitmap(exitBitmap, 15, 30);
            surface.RenderBitmap(arrowBitmap, 6, arrowPos);
            //for debug
            //surface.RenderNumber(4815, 5, 42, -1);
            surface.RenderBitmap(forIgdcBitmap, 4, 54);            
            surface.SwapBuffers();
        }

        public void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                    Current = (Current == MenuItem.Game) ? MenuItem.Exit : MenuItem.Game;
                    Render();
                    break;

                case Keys.Return:
                    switch (Current)
                    { 
                        case MenuItem.Game:
                            controller.SwitchTo(game);
                            break;
                        case MenuItem.Exit:
                            Application.Exit();
                            break;
                    }
                    break;

                default:
                    break;
            }            
        }

        public void Update(double dt)
        { }
    }
}
