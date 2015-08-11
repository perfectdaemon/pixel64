using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SharpPixel
{
    interface IRenderSurface
    {
        Bitmap BackBuffer { get; }
        Graphics BackGraphics { get; }
        void SetNumbersBitmap(Bitmap bitmap, int digitWidth);
        void SwapBuffers();
        void RenderBackground(Color color);
        void RenderBitmap(Bitmap bitmap, Point location);
        void RenderBitmap(Bitmap bitmap, int x, int y);
        void RenderNumber(int number, Point location, int interval = 0);
        void RenderNumber(int number, int x, int y, int interval = 0);
    }
}
