using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Logic.Interfaces.Unity.Enemy;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Logic.Unity.Enemy
{
    public class EnemyViewController : MonoBehaviour, IEnemyViewController
    {
        private static readonly int _color05 = Shader.PropertyToID("Color_C5D962E7");

        [SerializeField, Required]
        private SkinnedMeshRenderer _meshRenderer;

        private Color _defaultColor;
        private Sequence _blinkSequence;
        private CancellationToken _token;

        private void Awake()
        {
            _defaultColor = _meshRenderer.material.GetColor(_color05);
        }

        public void Blink(Color blinkColor)
        {
            if (_blinkSequence.IsActive())
            {
                return;
            }
            
            _blinkSequence = DOTween.Sequence();
            
            _blinkSequence
                .Append(_meshRenderer.material.DOColor(blinkColor, _color05, 0.2f))
                .Append(_meshRenderer.material.DOColor(_defaultColor, _color05, 0.1f))
                .Play();
        }

        public async UniTask PostAttack(float duration, CancellationToken token)
        {
            if (_blinkSequence.IsActive())
            {
                return;
            }
            
            _token = token;
            
            _blinkSequence = DOTween.Sequence();
            _blinkSequence.onUpdate += OnProcess;

            await _blinkSequence
                .Append(_meshRenderer.material.DOColor(Color.white, _color05, 0.1f))
                .Append(_meshRenderer.material.DOColor(_defaultColor, _color05, duration))
                .Play()
                .AsyncWaitForCompletion();
        }

        private void OnProcess()
        {
            if (_token.IsCancellationRequested)
            {
                _blinkSequence.Kill();
            }
        }
    }
}