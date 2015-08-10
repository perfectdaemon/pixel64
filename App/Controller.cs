using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SharpPixel
{
    class Controller : IController
    {
        private IGameMenu menu;
        private IGame game;
        private IRenderSurface surface;

        private IScene currentScene = null;

        public void Initialize(IRenderSurface surface)
        {
            this.surface = surface;

            game = new Game();
            game.SetController(this);
            game.Initialize(surface, menu);
            game.LoadResources();
            
            menu = new GameMenu();
            menu.SetController(this);
            menu.Initialize(surface, game);           
            menu.LoadResources();

            currentScene = menu;

            Render();
        }

        public void Update(double dt)
        {
            if (currentScene != null)
                currentScene.Update(dt);
        }

        public void HandleKeys(KeyEventArgs e)
        {
            if (currentScene != null)
                currentScene.HandleKeys(e);
        }

        public void Render()
        {
            if (currentScene != null)
                currentScene.RenderSelf();
        }

        public void SwitchTo(IScene scene)
        {
            if (scene != currentScene)
            {
                currentScene = scene;
                Render();
            }
        }
    }
}
