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
            if (_networkService.CurrentNetworkPlayer is null)
            {
                return;
            }

            var _netLeftHandData = _networkService.CurrentNetworkPlayer.LeftHandData;
            var _netRightHandData = _networkService.CurrentNetworkPlayer.RightHandData;

            _netLeftHandData.MapTransformWith(LeftHand.HandData);
            _netRightHandData.MapTransformWith(RightHand.HandData);
        }
    }
}
