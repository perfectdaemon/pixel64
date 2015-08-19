using System.Drawing;

namespace SharpPixel.Engine
{    
    /// <summary>
    /// Represents some surface with back and front buffers
    /// </summary>
    public interface IRenderSurface
    {
        /// <summary>
        /// Direct access to back buffer
        /// </summary>
        Bitmap BackBuffer { get; }

        /// <summary>
        /// Graphics object assosiated with back buffer. Use it if you need to draw something directly
        /// </summary>
        Graphics BackGraphics { get; }

        /// <summary>
        /// Sets bitmap that will be used to draw numbers. 
        /// Digits must be the same width with no interval and positioned at one line
        /// </summary>
        /// <param name="bitmap">Image that contains numbers</param>
        /// <param name="digitWidth">Width of one digit</param>
        void SetNumbersBitmap(Bitmap bitmap, int digitWidth);

        /// <summary>
        /// Draws backbuffer to screen
        /// </summary>
        void SwapBuffers();

        /// <summary>
        /// Renders screen sixed rectangle to back buffer with specified color
        /// </summary>
        /// <param name="color">Background color</param>
        void RenderBackground(Color color);

        /// <summary>
        /// Renders specified bitmap to backbuffer. 
        /// Pivot - top left corner of bitmap
        /// </summary>
        /// <param name="bitmap">Bitmap to draw</param>
        /// <param name="location">Draw position</param>
        void RenderBitmap(Bitmap bitmap, Point location);

        /// <summary>
        /// Renders specified bitmap to backbuffer. 
        /// Pivot - top left corner of bitmap
        /// </summary>
        /// <param name="bitmap">Bitmap to draw</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        void RenderBitmap(Bitmap bitmap, int x, int y);

        /// <summary>
        /// Renders specified bitmap to backbuffer. 
        /// Pivot - top left corner of bitmap
        /// </summary>
        /// <param name="bitmap">Bitmap to draw</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="rotation">rotation angle in degrees</param>
        void RenderBitmap(Bitmap bitmap, int x, int y, int rotation);

        /// <summary>
        /// Renders specified bitmap to backbuffer. 
        /// Pivot - top left corner of bitmap
        /// </summary>
        /// <param name="bitmap">Bitmap to draw</param>
        /// <param name="location">Draw position</param>
        /// <param name="rotation">rotation angle in degrees</param>
        void RenderBitmap(Bitmap bitmap, Point location, int rotation);

        /// <summary>
        /// Renders number using associated with SetNumbersBitmap() bitmap
        /// </summary>
        /// <param name="number">Number to draw</param>
        /// <param name="location">Draw position</param>
        /// <param name="interval">Interval between digits in pixels</param>
        void RenderNumber(int number, Point location, int interval = 0);

        /// <summary>
        /// Renders number using associated with SetNumbersBitmap() bitmap
        /// </summary>
        /// <param name="number">Number to draw</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="interval">Interval between digits in pixels</param>
        void RenderNumber(int number, int x, int y, int interval = 0);

        /// <summary>
        /// Immediately copies back buffer to new bitmap and returns it
        /// </summary>
        /// <returns>Bitmap with copy of back buffer</returns>
        Bitmap TakeScreenshot();
    }
}
