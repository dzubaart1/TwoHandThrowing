using UnityEngine;

namespace TwoHandThrowing.Player
{
    public class LocalPlayer : MonoBehaviour
    {
        public Transform Head { get; private set; }
        public HandCollision LeftHand { get; private set; }
        public HandCollision RightHand { get; private set; }

        [SerializeField] private HandCollision _leftHand;
        [SerializeField] private HandCollision _rightHand;
        [SerializeField] private Transform _head;

        private void Awake()
        {
            Head = _head;
            LeftHand = _leftHand;
            RightHand = _rightHand;
        }

        public void ToggleHandVisual(HandType handType, bool isVisible)
        {
            HandData hand = handType == HandType.Left ? LeftHand.HandData : RightHand.HandData;
            hand.ToggleHandVisible(isVisible);
        }
    }
}
