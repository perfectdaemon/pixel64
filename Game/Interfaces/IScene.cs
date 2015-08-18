using System.Windows.Forms;

namespace SharpPixel
{
    interface IScene
    {
        void SetController(IController controller);
        void SetRenderSurface(IRenderSurface surface);        
        
        void LoadResources();

        void Reset();
        
        void Render();
        void OnKeyDown(KeyEventArgs e);
        void Update(double dt);
    }
}
