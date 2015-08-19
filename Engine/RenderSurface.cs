using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SharpPixel.Engine
{
    public class RenderSurface : UserControl, IRenderSurface
    {
        private Bitmap backbuffer = new Bitmap(Utility.FIELD_SIZE, Utility.FIELD_SIZE);
        private Bitmap numbersBitmap;
        private int digitWidth;
        private Graphics bbGraphics;
        private Rectangle rect;
        private SolidBrush brush = new SolidBrush(Color.Black);

        public RenderSurface()            
        { 
            rect = new Rectangle(0, 0, backbuffer.Width, backbuffer.Height);
            
            bbGraphics = Graphics.FromImage(backbuffer);
            bbGraphics.SmoothingMode = SmoothingMode.None;
            bbGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            bbGraphics.PixelOffsetMode = PixelOffsetMode.None;
            bbGraphics.PageUnit = GraphicsUnit.Pixel;
            this.DoubleBuffered = true;

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);

            RenderBackground(Color.Black);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            
        }        

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            e.Graphics.SmoothingMode = SmoothingMode.None;            
            e.Graphics.DrawImage(backbuffer, new Rectangle(new Point(0, 0), Utility.WindowSize), new Rectangle(0, 0, backbuffer.Width, backbuffer.Height), GraphicsUnit.Pixel);
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }  

        public Bitmap BackBuffer { get { return backbuffer; } }

        public Graphics BackGraphics { get { return bbGraphics; } }

        public void SetNumbersBitmap(Bitmap bitmap, int digitWidth)
        {
            this.numbersBitmap = bitmap;
            this.digitWidth = digitWidth;
        }

        public void SwapBuffers()
        {
            this.Invalidate();
        }

        public void RenderBitmap(Bitmap bitmap, Point location)
        {
            this.RenderBitmap(bitmap, location.X, location.Y);            
        }

        public void RenderBitmap(Bitmap bitmap, int x, int y)
        {
            bbGraphics.DrawImage(bitmap, new Rectangle(x, y, bitmap.Width, bitmap.Height));
        }

        public void RenderBitmap(Bitmap bitmap, int x, int y, int rotation)
        {
            bbGraphics.TranslateTransform(x, y);            
            bbGraphics.RotateTransform(rotation);            
            bbGraphics.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            bbGraphics.ResetTransform();
        }

        public void RenderBitmap(Bitmap bitmap, Point location, int rotation)
        {
            this.RenderBitmap(bitmap, location.X, location.Y, rotation);
        }

        public void RenderBackground(Color color)
        {
            brush.Color = color;
            bbGraphics.FillRectangle(brush, rect);
        }

        public void RenderNumber(int number, Point location, int interval = 0)
        {
            this.RenderNumber(number, location.X, location.Y, interval);
        }

        public void RenderNumber(int number, int x, int y, int interval = 0)
        {
            string numberStr = number.ToString();
            int xOffset = 0;
            foreach (char digitChar in numberStr)
            {
                int digit = int.Parse(digitChar.ToString());
                bbGraphics.DrawImage(numbersBitmap,
                    new Rectangle(x + xOffset, y, digitWidth, numbersBitmap.Height), 
                    new Rectangle(digit * digitWidth, 0, digitWidth, numbersBitmap.Height), 
                    GraphicsUnit.Pixel);
                xOffset += digitWidth + interval;
            }
        }

        public Bitmap TakeScreenshot()
        {
            var bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.SmoothingMode = SmoothingMode.None;
                g.DrawImage(backbuffer, new Rectangle(new Point(0, 0), Utility.WindowSize), new Rectangle(0, 0, backbuffer.Width, backbuffer.Height), GraphicsUnit.Pixel);
            }
            return bitmap;
        }
    }
}
