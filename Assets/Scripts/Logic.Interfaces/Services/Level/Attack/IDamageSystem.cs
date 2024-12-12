using Logic.Interfaces.Unity;

namespace Logic.Interfaces.Services.Level.Attack
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
