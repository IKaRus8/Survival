using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Services.Level.Attack;

namespace Logic.Services.Level.Attack
{
    public class AttackService : IAttackService
    {
        private readonly IAttackModelsProvider _attackModelsProvider;
        private readonly IDamageSystem _damageSystem;

        public AttackService(
            IAttackModelsProvider attackModelsProvider,
            IDamageSystem damageSystem)
        {
            _attackModelsProvider = attackModelsProvider;
            _damageSystem = damageSystem;
        }

        public void AttackProcess(string attackModelId)
        {
            var model = _attackModelsProvider.GetAttackModel(attackModelId);
            
            _damageSystem.ToHero().Do(model.Damage);
        }
    }
}