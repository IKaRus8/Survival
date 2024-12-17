using Logic.Interfaces.Unity;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class HpBarView : MonoBehaviour
    {
        [SerializeField, Required]
        private Image _value;

        protected IDamageable _target;
        protected float _maxHp;
        
        private void LateUpdate()
        {
            if (_target == null)
            {
                return;
            }
            
            FillValue(_target.Health / _maxHp);
        }

        protected void FillValue(float value)
        {
            _value.fillAmount = value;
        }
    }
}