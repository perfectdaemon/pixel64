using System;
using System.Windows.Forms;
using SharpPixel.Engine;

namespace SharpPixel.Game.Interfaces
{
    public interface IController
    {
        void SetRenderSurface(IRenderSurface surface);
        void SetSound(ISound sound);

        void Start();

        void OnKeyDown(KeyEventArgs e);
        void Render();
        void Update(double dt);

        void SwitchTo(IScene scene);
    }
}
