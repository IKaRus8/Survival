using Cysharp.Threading.Tasks;
using Logic.Interfaces.Unity.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Logic.Unity.Player
{
    public class DefendObject : MonoBehaviour, IDefendObject
    {
        private static readonly int _die = Animator.StringToHash("die");
        private static readonly int _getHit = Animator.StringToHash("get_hit");

        [SerializeField, Required]
        private Animator _animator;

        private bool _isHit;

        public float Health { get; private set; } = 500f;
        public bool IsDead => Health <= 0;
        public Vector3 Position {get; private set;}

        private void Awake()
        {
            Position = transform.position;
        }
        
        public void TakeDamage(float damage)
        {
            Health -= damage;
            
            GetHit().Forget();
        }

        public void Heal(float healAmount)
        {
            Health += healAmount;
        }

        public async UniTask Die()
        {
            _animator.SetTrigger(_die);

            await UniTask.Delay(2000);
        }

        private async UniTaskVoid GetHit()
        {
            if (_isHit)
            {
                return;
            }
            
            _isHit = true;
            _animator.SetTrigger(_getHit);

            await UniTask.Delay(800);
            
            _isHit = false;
            _animator.ResetTrigger(_getHit);
        } 
    }
}