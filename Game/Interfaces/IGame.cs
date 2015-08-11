namespace SharpPixel
{
    interface IGame : IScene
    {        
        void Initialize(IMainMenu mainMenu, IGameOverMenu gameOverMenu);        
    }
}
