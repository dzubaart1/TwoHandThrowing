using UnityEngine;

namespace VrGunTest.Scripts.LookAt
{
    public class LookAtMainCamera : MonoBehaviour
    {
        private int _frameCounter;
        
        private Transform _mainCameraTransform;
        
        public void SetCameraTransform(Transform target)
        {
            _mainCameraTransform = target;
            enabled = true;
        }
        
        private void Update()
        {
            transform.LookAt(_mainCameraTransform, Vector3.up);
        }
    }
}