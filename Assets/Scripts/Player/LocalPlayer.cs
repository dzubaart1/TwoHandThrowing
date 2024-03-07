using UnityEngine;

namespace TwoHandThrowing.Player
{
    public class LocalPlayer : MonoBehaviour
    {
        public Transform Head => _head;
        public HandCollision LeftHand => _leftHand;
        public HandCollision RightHand => _rightHand;

        [SerializeField] private HandCollision _leftHand;
        [SerializeField] private HandCollision _rightHand;
        [SerializeField] private Transform _head;
    }
}
