namespace SharpPixel
{
    interface IGameOverMenu : IScene
    {
        void Initialize(IGameScene game);
        void SetScore(int score);
    }
}
