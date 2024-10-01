using System;
using Logic.Interfaces;
using UnityEngine;

namespace Logic
{
    public class GridElement : MonoBehaviour, IGridElement
    {
        [SerializeField]
        private Transform _transform;
        [SerializeField]
        private BoxCollider _collider;

        public Transform Transform => _transform;
        public BoxCollider Collider => _collider;
        public Action<IGridElement> OnPlayerEnter { get; set; }
        public bool IsPlayerInside { get; private set; }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Reset()
        {
            IsPlayerInside = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerEnter();
            }
        }

        private void PlayerEnter()
        {
            OnPlayerEnter?.Invoke(this);
            
            IsPlayerInside = true;
        }
    }
}