
namespace Data.Models.Enemy
{
    public struct EnemySpawnParameter
    {
        public int Quantity { get; }
        public float Chance { get; }
        
        public EnemySpawnParameter(int quantity, int chance)
        {
            Quantity = quantity;
            Chance = chance * 0.01f;
        }
    }
}