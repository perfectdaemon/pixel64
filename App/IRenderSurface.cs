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
        void SwapBuffers();
        void RenderBackground(Color color);
        void RenderBitmap(Bitmap bitmap, Point location);
        void RenderBitmap(Bitmap bitmap, int x, int y);
    }
}
