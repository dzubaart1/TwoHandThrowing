using UnityEngine;

namespace VrGunTest.Scripts
{
    public class VrRigReference : MonoBehaviour
    {
        [Header("Точки синхронизации с сетевым игроков")]
        public Transform Root;
        public Transform Head;
        public Transform RightController;
        public Transform LeftController;
    }
}