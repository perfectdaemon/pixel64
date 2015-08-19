namespace SharpPixel.Game.Interfaces
{
    public interface IGameOverMenu : IScene
    {
        void Initialize(IGameScene game);
        void SetScore(int score);
    }
}
