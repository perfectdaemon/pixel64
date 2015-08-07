using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SharpPixel
{
    public interface IRenderSurface
    {
        Bitmap BackBuffer { get; }
        Graphics BackGraphics { get; }
        void SwapBuffers();
        void RenderBackground(Color color);
        void RenderBitmap(Bitmap bitmap, Point location);
        void RenderBitmap(Bitmap bitmap, int x, int y);
    }

    public class RenderSurface : UserControl, IRenderSurface
    {
        private Bitmap backbuffer = new Bitmap(Utility.FIELD_SIZE, Utility.FIELD_SIZE);
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

        public Bitmap BackBuffer { get { return backbuffer; } }

        public Graphics BackGraphics { get { return bbGraphics; } }

        public void SwapBuffers()
        {
            this.Invalidate();
        }

        public void RenderBitmap(Bitmap bitmap, Point location)
        {
            bbGraphics.DrawImage(bitmap, new Rectangle(location.X, location.Y, bitmap.Width, bitmap.Height));
        }

        public void RenderBitmap(Bitmap bitmap, int x, int y)
        {
            bbGraphics.DrawImage(bitmap, new Rectangle(x, y, bitmap.Width, bitmap.Height));
        }

        public void RenderBackground(Color color)
        {
            brush.Color = color;
            bbGraphics.FillRectangle(brush, rect);
        }
    }
}
