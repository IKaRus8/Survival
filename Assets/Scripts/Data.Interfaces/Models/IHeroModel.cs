
namespace Data.Interfaces.Models
{
    public interface IHeroModel
    {
        string Id { get; }
        float Health { get; }
        float Speed { get; }
        string AttackModelId { get; }
    }
}