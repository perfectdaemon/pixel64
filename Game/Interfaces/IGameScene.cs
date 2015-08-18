namespace SharpPixel
{
    interface IGameScene : IScene
    {        
        void Initialize(IMainMenu mainMenu, IGameOverMenu gameOverMenu);        
    }
}
