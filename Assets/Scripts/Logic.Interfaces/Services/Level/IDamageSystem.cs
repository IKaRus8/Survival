using Logic.Interfaces.Unity;

namespace Logic.Interfaces.Services.Level
{
    public interface IDamageSystem 
    {
        IDamageSystem FromHero();

        IDamageSystem FromEnemy();

        IDamageSystem ToEnemy(IEnemy enemy);

        IDamageSystem ToHero();

        void Do(float damage);
    }
}
