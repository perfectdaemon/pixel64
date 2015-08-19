namespace SharpPixel.Game.Interfaces
{
    public interface IGameScene : IScene
    {        
        void Initialize(IMainMenu mainMenu, IGameOverMenu gameOverMenu);        
    }
}
