using System;
using UnityEngine;

namespace VrGunTest.Scripts
{
    public class ObjectCatcher : MonoBehaviour
    {
        public event Action Caught;
        
        [SerializeField] private LayerMask _layerMask;
        
        private void OnTriggerEnter(Collider other)
        {
            if (_layerMask == (_layerMask | (1 << other.gameObject.layer)))
                Caught?.Invoke();
        }
    }
}