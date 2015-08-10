using System.Windows.Forms;

namespace SharpPixel
{
    interface IGame : IScene
    {        
        void Initialize(IRenderSurface surface, IGameMenu menu);        
    }
}
