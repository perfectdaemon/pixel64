namespace SharpPixel
{
    interface IGameOverMenu : IScene
    {
        void Initialize(IGame game);
        void SetScore(int score);
    }
}
