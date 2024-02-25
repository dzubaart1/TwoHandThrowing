using TwoHandThrowing.Core;
using UnityEngine;

namespace TwoHandThrowing.Player
{
    public class LocalPlayer : MonoBehaviour
    {
        public Transform Head => _head;
        public HandRef LeftHand => _leftHand;
        public HandRef RightHand => _rightHand;

        [SerializeField] private HandRef _leftHand;
        [SerializeField] private HandRef _rightHand;
        [SerializeField] private Transform _head;

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

            MapTransform(Head, _networkService.CurrentNetworkPlayer.Head);
        }

        private void MapTransform(Transform from, Transform to)
        {
            to.rotation = from.rotation;
            to.position = from.position;
        }
    }
}
