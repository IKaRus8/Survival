using Logic.Interfaces.Unity.Player;
using Zenject;

namespace UI.View
{
    public class DefendObjectHpBarView : HpBarView
    {
        [Inject]
        private void Construct(IDefendObject target)
        {
            _maxHp = target.Health;
            _target = target;
        }
    }
}