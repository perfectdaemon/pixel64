using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SharpPixel.Engine;
using SharpPixel.Game.Interfaces;

namespace SharpPixel.Game
{
    public abstract class Scene : IScene
    {
        protected IRenderSurface surface;
        protected IController controller;
        protected ISound sound;

        public void SetController(IController controller)
        {
            this.controller = controller;
        }

        public void SetRenderSurface(IRenderSurface surface)
        {
            this.surface = surface;
        }

        public void SetSound(ISound sound)
        {
            this.sound = sound;
        }

        public abstract void LoadResources();        
        public abstract void Render();
        public abstract void OnKeyDown(KeyEventArgs e);
        
        public virtual void Update(double dt)
        { }

        public virtual void Reset()
        { }
    }
}
