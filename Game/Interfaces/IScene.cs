using SharpPixel.Engine;
using System.Windows.Forms;

namespace SharpPixel.Game.Interfaces
{
    public interface IScene
    {
        void SetController(IController controller);
        void SetRenderSurface(IRenderSurface surface);
        void SetSound(ISound sound);
        
        void LoadResources();

        void Reset();
        
        void Render();
        void OnKeyDown(KeyEventArgs e);
        void Update(double dt);
    }
}
