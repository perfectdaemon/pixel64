using System.Windows.Forms;
using System.Drawing.Imaging;
using SharpPixel.Engine;
using SharpPixel.Game.Interfaces;
using System;

namespace SharpPixel.Game
{
    public class Controller : IController
    {
        private IMainMenu menu;
        private IGameScene game;
        private IGameOverMenu gameOver;
        private IRenderSurface surface;
        private ISound sound;

        private IScene currentScene = null;

        private bool doTakeScreenshot = false;

        private void TakeAndSaveScreenshot()
        {
            var screenshot = surface.TakeScreenshot();
            screenshot.Save(string.Format("scr_{0}.png", DateTime.Now.ToString("yyyyMMdd_HH-mm-ss")), ImageFormat.Png);
        }

        public void SetSound(ISound sound)
        {
            this.sound = sound;
        }

        public void SetRenderSurface(IRenderSurface surface)
        {
            this.surface = surface;
            surface.SetNumbersBitmap(ResourceManager.GetBitmapResource("Numbers"), 5);
        }

        public void Start()
        {
            gameOver = new GameOverMenu();
            game = new GameScene();
            menu = new MainMenu();

            game.SetController(this);
            game.SetRenderSurface(surface);
            game.SetSound(sound);
            game.Initialize(menu, gameOver);
            game.LoadResources();
            game.Reset();

            menu.SetController(this);
            menu.SetRenderSurface(surface);
            menu.SetSound(sound);
            menu.Initialize(game);
            menu.LoadResources();

            gameOver.SetController(this);
            gameOver.SetRenderSurface(surface);
            gameOver.SetSound(sound);
            gameOver.Initialize(game);
            gameOver.LoadResources();

            currentScene = menu;

            Render();
        }

        public void Update(double dt)
        {
            if (currentScene != null)
                currentScene.Update(dt);
            if (doTakeScreenshot)
            {
                TakeAndSaveScreenshot();
                doTakeScreenshot = false;
            }
        }

        public void OnKeyDown(KeyEventArgs e)
        {
            if (currentScene != null)
                currentScene.OnKeyDown(e);
            if (e.KeyCode == Keys.F12)
                doTakeScreenshot = true;
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
