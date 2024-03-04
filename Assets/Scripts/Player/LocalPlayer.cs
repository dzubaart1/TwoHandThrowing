using TwoHandThrowing.Core;
using UnityEngine;

namespace TwoHandThrowing.Player
{
    public class LocalPlayer : MonoBehaviour
    {
        public HandRef LeftHand => _leftHand;
        public HandRef RightHand => _rightHand;

        [SerializeField] private HandRef _leftHand;
        [SerializeField] private HandRef _rightHand;

        private NetworkService _networkService;

        private void Awake()
        {
            _networkService = Engine.GetService<NetworkService>();
        }

        private void Update()
        {
            if (_networkService.CurrentNetworkPlayer == null)
            {
                return;
            }

            HandData netLeftHandData = _networkService.CurrentNetworkPlayer.LeftHandData;
            HandData netRightHandData = _networkService.CurrentNetworkPlayer.RightHandData;

            netLeftHandData.MapTransformWithHand(LeftHand.HandData);
            netRightHandData.MapTransformWithHand(RightHand.HandData);
        }
    }
}
