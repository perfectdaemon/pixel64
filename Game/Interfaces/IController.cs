using System;
using System.Windows.Forms;

namespace SharpPixel
{
    interface IController
    {
        void SetRenderSurface(IRenderSurface surface);
        void OnKeyDown(KeyEventArgs e);
        void Render();
        void Update(double dt);

        void SwitchTo(IScene scene);
    }
}
