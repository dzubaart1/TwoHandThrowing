using UnityEngine;

namespace VrGunTest.Scripts.LookAt
{
    public class MainCameraVisitor : MonoBehaviour
    {
        private void Start()
        {
            LookAtMainCamera[] findObjectByType = FindObjectsByType<LookAtMainCamera>(FindObjectsSortMode.None);
            foreach (var lookAtTarget in findObjectByType)
            {
                lookAtTarget.SetCameraTransform(transform);
            }
        }
    }
}