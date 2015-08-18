using System.Windows.Forms;
using SharpPixel.Game;

namespace SharpPixel
{
    class Controller : IController
    {
        private IMainMenu menu;
        private IGameScene game;
        private IGameOverMenu gameOver;
        private IRenderSurface surface;

        private IScene currentScene = null;

        public void SetRenderSurface(IRenderSurface surface)
        {
            this.surface = surface;
            surface.SetNumbersBitmap(ResourceManager.GetBitmapResource("Numbers"), 5);

            gameOver = new GameOverMenu();
            game = new GameScene();
            menu = new MainMenu();
            
            game.SetController(this);
            game.SetRenderSurface(surface);
            game.Initialize(menu, gameOver);
            game.LoadResources();
            game.Reset();
            
            menu.SetController(this);
            menu.SetRenderSurface(surface);
            menu.Initialize(game);           
            menu.LoadResources();

            gameOver.SetController(this);
            gameOver.SetRenderSurface(surface);
            gameOver.Initialize(game);
            gameOver.LoadResources();
            
            currentScene = menu;

            Render();
        }

        public void Update(double dt)
        {
            if (currentScene != null)
                currentScene.Update(dt);
        }

        public void OnKeyDown(KeyEventArgs e)
        {
            if (currentScene != null)
                currentScene.OnKeyDown(e);
        }

        public void Render()
        {
            if (currentScene != null)
                currentScene.Render();
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
