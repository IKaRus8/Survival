
using Cysharp.Threading.Tasks;

namespace Logic.Interfaces.Unity
{
    public interface IDamageable
    {
        float Health { get; }
        bool IsDead { get; }

        void TakeDamage(float damage);
        void Heal(float healAmount);
        UniTask Die();
    }
}