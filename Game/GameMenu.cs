using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SharpPixel
{
    class GameMenu : IGameMenu
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

        public void Initialize(IRenderSurface surface, IGame game)
        {
            this.surface = surface;
            this.game = game;
        }

        public void LoadResources()
        {
            try
            {
                gameBitmap = new Bitmap(Utility.GetResourcePath("game"));
                exitBitmap = new Bitmap(Utility.GetResourcePath("exit"));
                forIgdcBitmap = new Bitmap(Utility.GetResourcePath("igdc"));
                arrowBitmap = new Bitmap(Utility.GetResourcePath("arrow"));
                paletteBitmap = new Bitmap(Utility.GetResourcePath("palette"));
                arrowPos = 16;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке ресурсов меню: " + ex.ToString());
            }
        }

        public void RenderSelf()
        {            
            surface.RenderBackground(Color.FromArgb(98, 98, 98));
            surface.RenderBitmap(paletteBitmap, 1, 1);
            surface.RenderBitmap(gameBitmap, 15, 15);
            surface.RenderBitmap(exitBitmap, 15, 30);
            surface.RenderBitmap(arrowBitmap, 6, arrowPos);
            surface.RenderBitmap(forIgdcBitmap, 4, 54);
            surface.SwapBuffers();
        }

        public void HandleKeys(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                    Current = (Current == MenuItem.Game) ? MenuItem.Exit : MenuItem.Game;
                    RenderSelf();
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
