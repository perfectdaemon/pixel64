using System;
using System.Windows.Forms;

namespace SharpPixel
{
    interface IGameMenu : IScene
    {
        void Initialize(IRenderSurface surface, IGame game);        
    }
}
