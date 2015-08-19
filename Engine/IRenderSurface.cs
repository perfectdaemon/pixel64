using System.Drawing;

namespace SharpPixel.Engine
{
    public interface IRenderSurface
    {
        Bitmap BackBuffer { get; }
        Graphics BackGraphics { get; }
        void SetNumbersBitmap(Bitmap bitmap, int digitWidth);
        void SwapBuffers();
        void RenderBackground(Color color);
        void RenderBitmap(Bitmap bitmap, Point location);
        void RenderBitmap(Bitmap bitmap, int x, int y);
        void RenderBitmap(Bitmap bitmap, int x, int y, int rotation);
        void RenderBitmap(Bitmap bitmap, Point location, int rotation);
        void RenderNumber(int number, Point location, int interval = 0);
        void RenderNumber(int number, int x, int y, int interval = 0);
        Bitmap TakeScreenshot();
    }
}
