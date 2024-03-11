using Mirror;
using TwoHandThrowing.Core;
using TwoHandThrowing.Player;
using UnityEngine;

namespace TwoHandThrowing.Network
{
    public class NetworkPlayer : NetworkBehaviour
    {
        public Transform LeftHand => _leftHand;
        public Transform RightHand => _rightHand;
        
        [SerializeField] private HandData _leftHandData;
        [SerializeField] private HandData _rightHandData;
        [SerializeField] private Transform _head;
        [SerializeField] private Transform _leftHand;
        [SerializeField] private Transform _rightHand;

        private InputService _inputService;
        
        private void Awake()
        {
            _inputService = Engine.GetService<InputService>();
        }
        

        public override void OnStartClient()
        {
            base.OnStartClient();
            CmdSyncVisualHand();
            Debug.Log("SYNC");
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
            HandCollision localLeft = _inputService.LocalPlayer.LeftHand;
            HandCollision localRight = _inputService.LocalPlayer.RightHand;

            MapTransform(localLeft.transform, LeftHand);
            MapTransform(localRight.transform, RightHand);
            MapTransform(localPlayer, transform);
        }
        
        private void MapTransform(Transform from, Transform to)
        {
            to.rotation = from.rotation;
            to.position = from.position;
        }

        [Command]
        public void CmdHideHand(HandType handType)
        {
            RpcHideHand(handType);
        }

        [ClientRpc]
        private void RpcHideHand(HandType handType)
        {
            HandData hand = _leftHandData;

            if (handType == HandType.Right)
            {
                hand = _rightHandData;
            }

            hand.HideHand();
        }
        
        [Command(requiresAuthority = false)]
        public void CmdShowHand(HandType handType)
        {
            RpcShowHand(handType);
        }

        [ClientRpc]
        private void RpcShowHand(HandType handType)
        {
            HandData hand = _leftHandData;

            if (handType == HandType.Right)
            {
                hand = _rightHandData;
            }

            hand.ShowHand();
        }
        
        [Command(requiresAuthority = false)]
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

        [Command(requiresAuthority = false)]
        private void CmdSyncVisualHand()
        {
            RpcChangeNetHandDataType(HandType.Left, _leftHandData.HandDataType);
            RpcChangeNetHandDataType(HandType.Right, _rightHandData.HandDataType);
        }
    }
}