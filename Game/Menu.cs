using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SharpPixel
{
    public class GameMenu
    {
        private Bitmap game, exit, forIgdc, arrow, palette;
        private IRenderSurface surface;
        private int arrowPos;

        public GameMenu(IRenderSurface surface)
        {
            this.surface = surface;
        }

        public void LoadResources()
        {
            try
            {
                game = new Bitmap(Utility.GetResourcePath("game"));
                exit = new Bitmap(Utility.GetResourcePath("exit"));
                forIgdc = new Bitmap(Utility.GetResourcePath("igdc"));
                arrow = new Bitmap(Utility.GetResourcePath("arrow"));
                palette = new Bitmap(Utility.GetResourcePath("palette"));
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
            surface.RenderBitmap(palette, 1, 1);
            surface.RenderBitmap(game, 15, 15);
            surface.RenderBitmap(exit, 15, 30);
            surface.RenderBitmap(arrow, 6, arrowPos);
            surface.RenderBitmap(forIgdc, 4, 54);
            surface.SwapBuffers();
        }

        public void HandleKeys(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                    arrowPos = arrowPos == 16 ? 31 : 16;
                    RenderSelf();
                    break;

                case Keys.Return:
                    break;

                default:
                    break;
            }            
        }
    }
}
