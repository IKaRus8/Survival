namespace Logic.RuntimeData
{
    public class LevelParameters
    {
        public bool IsTimeLevel { get; private set; }

        public LevelParameters SetAsTimeLevel()
        {
            IsTimeLevel = true;
            
            return this;
        }
    }
}