using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SharpPixel
{
    interface IScene
    {
        void SetController(IController controller);
        void HandleKeys(KeyEventArgs e);
        void LoadResources();
        void RenderSelf();
        void Update(double dt);
    }
}
