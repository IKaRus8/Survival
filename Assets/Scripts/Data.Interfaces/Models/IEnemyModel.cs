
namespace Data.Interfaces.Models
{
    public interface IEnemyModel
    {
        string Id { get; }
        float Health { get; }
        float MoveSpeed { get; }
        string AttackModelId { get; }
    }
}