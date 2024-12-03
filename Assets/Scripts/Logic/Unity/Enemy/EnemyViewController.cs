using DG.Tweening;
using Logic.Interfaces.Unity.Enemy;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Logic.Unity.Enemy
{
    public class EnemyViewController : MonoBehaviour, IEnemyViewController
    {
        [SerializeField, Required]
        private MeshRenderer _meshRenderer;

        private Color _defaultColor;
        private Sequence _blinkSequence;

        private void Awake()
        {
            _defaultColor = _meshRenderer.material.color;
        }

        public void Blink(Color blinkColor)
        {
            if (_blinkSequence.IsActive())
            {
                return;
            }
            
            _blinkSequence = DOTween.Sequence();
            
            _blinkSequence
                .Append(_meshRenderer.material.DOColor(blinkColor, 0.2f))
                .Append(_meshRenderer.material.DOColor(_defaultColor, 0.1f))
                .Play();
        }
    }
}