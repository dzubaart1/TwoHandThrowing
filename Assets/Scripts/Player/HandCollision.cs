using System;
using UnityEngine;

namespace TwoHandThrowing.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class HandCollision : MonoBehaviour
    {
        [SerializeField] private Collider[] _handColliders;
        
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        [ContextMenu("Config Hand Collisions")]
        private void CollectCollider()
        {
            _handColliders = gameObject.GetComponentsInChildren<Collider>();
        }

        public void TurnOffColliders()
        {
            foreach (var collider in _handColliders)
            {
                collider.enabled = false;
            }
            
            _rigidbody.Sleep();
        }

        public void TurnOnColliders()
        {
            foreach (var collider in _handColliders)
            {
                collider.enabled = true;
            }
            
            _rigidbody.WakeUp();
        }

        public void UpdatePhysicMaterial(PhysicMaterial physicMaterial)
        {
            foreach (var collider in _handColliders)
            {
                collider.material = physicMaterial;
            }
        }
    }
}
