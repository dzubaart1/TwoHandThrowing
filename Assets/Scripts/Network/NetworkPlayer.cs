using Mirror;
using TwoHandThrowing.Core;
using TwoHandThrowing.Player;
using UnityEngine;

namespace TwoHandThrowing.Network
{
    public class NetworkPlayer : NetworkBehaviour
    {
        public HandData LeftHandData => _leftHandData;
        public HandData RightHandData => _rightHandData;

        [SerializeField] private HandData _leftHandData;
        [SerializeField] private HandData _rightHandData;
        [SerializeField] private Transform _head;

        private InputService _inputService;
        
        private void Awake()
        {
            _inputService = Engine.GetService<InputService>();
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            if (!isOwned | Engine.GetService<NetworkService>().CurrentNetworkPlayer != null)
            {
                return;
            }
            
            Engine.GetService<NetworkService>().SetCurrentNetworkPlayer(this);
            _leftHandData.gameObject.SetActive(false);
            _rightHandData.gameObject.SetActive(false);
            _head.gameObject.SetActive(false);
        }
        
        private void Update()
        {
            if (!isOwned)
            {
                return;
            }
            
            Transform localPlayer = _inputService.LocalPlayer.transform;
            HandData localLeftHandData = _inputService.LocalPlayer.LeftHand.HandData;
            HandData localRightHandData = _inputService.LocalPlayer.RightHand.HandData;

            MapTransform(localLeftHandData.Root.parent, _leftHandData.Root.parent);
            MapTransform(localLeftHandData.Root, _leftHandData.Root);
            
            MapTransform(localRightHandData.Root.parent, _rightHandData.Root.parent);
            MapTransform(localRightHandData.Root, _rightHandData.Root);

            transform.rotation = localPlayer.rotation;
            transform.position = localPlayer.position;
        }
        
        private void MapTransform(Transform from, Transform to)
        {
            to.localRotation = from.localRotation;
            to.localPosition = from.localPosition;
        }
        

        [Command]
        public void CmdChangeNetHandDataType(HandType handType, HandDataType handDataType)
        {
            RpcChangeNetHandDataType(handType, handDataType);
        }

        [ClientRpc]
        private void RpcChangeNetHandDataType(HandType handType, HandDataType handDataType)
        {
            switch (handType)
            {
                case HandType.Left:
                    _leftHandData.UpdateHandDataType(handDataType);
                    break;
                case HandType.Right:
                    _rightHandData.UpdateHandDataType(handDataType);
                    break;
            }
        }
    }
}