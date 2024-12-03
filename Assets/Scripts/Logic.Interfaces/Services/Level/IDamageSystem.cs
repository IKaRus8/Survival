using Logic.Interfaces.Unity;
using Logic.Interfaces.Unity.Enemy;

namespace Logic.Interfaces.Services.Level
{
    public interface IDamageSystem 
    {
        IDamageSystem FromHero();

        IDamageSystem FromEnemy();

        IDamageSystem ToTarget(IDamageable target);

        IDamageSystem ToHero();

        void Do(float damage);
    }
}
