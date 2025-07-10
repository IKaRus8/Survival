using Cysharp.Threading.Tasks;
using Data.Interfaces.Enums;
using DG.Tweening;
using Logic.Interfaces.Unity;
using UnityEngine;

namespace Logic.Unity.Orbs
{
    public class HealOrb : MonoBehaviour, IOrb
    {
        private const float FlightDuration = 0.3f;
        private const float ArcHeight = 2f;
        private const float RandomSpread = 0.5f;
        
        private Sequence _sequence;
        
        public OrbTypes OrbType => OrbTypes.Heal;
        public Vector3 Position { get; private set; }
        public bool IsTaken { get; private set; }

        private void Start()
        {
            Position = transform.position;
        }

        public void MoveTo(Vector3 position)
        {
            transform.position = position;
        }
        
        public async UniTask FlyTo(Vector3 endPosition)
        {
            if (IsTaken)
            {
                return;
            }
            
            IsTaken = true;
            
            // Случайное смещение для начала дуги
            Vector3 start = transform.position;
        
            Vector3 mid = (start + endPosition) * 0.5f;
            mid.y += ArcHeight;

            // Добавим небольшой рандом в дугу
            mid.x += Random.Range(-RandomSpread, RandomSpread);
            mid.y += Random.Range(-RandomSpread, RandomSpread);
            mid.z += Random.Range(-RandomSpread, RandomSpread);

            _sequence?.Kill();

            _sequence = DOTween.Sequence()
                .Append(transform.DOMove(mid, FlightDuration * 0.5f).SetEase(Ease.OutQuad))
                .Append(transform.DOMove(endPosition, FlightDuration * 0.5f).SetEase(Ease.InQuad));
            
            await _sequence.AsyncWaitForCompletion();
            
            DestroyOrb();
        }

        public void DestroyOrb()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}