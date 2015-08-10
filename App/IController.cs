using System;
using System.Windows.Forms;

namespace SharpPixel
{
    interface IController
    {
        void HandleKeys(KeyEventArgs e);
        void Initialize(IRenderSurface surface);
        void Render();
        void SwitchTo(IScene scene);
        void Update(double dt);
    }
}
