namespace Logic.Interfaces.Services.Level
{
    public interface IGameOverService
    {
        void LevelComplete();
        void LevelFailed();
    }
}