using Mirror;
using TwoHandThrowing.Core;
using TwoHandThrowing.Player;
using UnityEngine;

namespace TwoHandThrowing.Network
{
    public class NetworkPlayer : NetworkBehaviour
    {
        public Transform LeftHand { get; private set; }
        public Transform RightHand { get; private set; }
        
        [SerializeField] private HandData _leftHandData;
        [SerializeField] private HandData _rightHandData;
        [SerializeField] private Transform _head;
        [SerializeField] private Transform _leftHand;
        [SerializeField] private Transform _rightHand;
        
        private NetworkService _networkService;
        private InputService _inputService;
        
        private LocalPlayer _localPlayer;
        
        private void Awake()
        {
            _inputService = Engine.GetService<InputService>();
            _networkService = Engine.GetService<NetworkService>();

            LeftHand = _leftHand;
            RightHand = _rightHand;

            _localPlayer = _inputService.LocalPlayer;
        }
        
        public override void OnStartClient()
        {
            base.OnStartClient();
            
            CmdSyncVisualHand();

            if (!isOwned | _networkService.CurrentNetworkPlayer != null)
            {
                return;
            }
            
            _networkService.SetCurrentNetworkPlayer(this);
            
            // Отключаем видимую часть
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
            
            // Синхронизируем позиции
            MapTransform(_localPlayer.LeftHand.transform, LeftHand);
            MapTransform(_localPlayer.RightHand.transform, RightHand);
            MapTransform(_localPlayer.transform, transform);
        }
        
        
        private void MapTransform(Transform from, Transform to)
        {
            to.rotation = from.rotation;
            to.position = from.position;
        }

        [Command(requiresAuthority = false)]
        public void CmdVisualToggleHand(HandType handType, bool isVisible)
        {
            RpcVisualToggleHand(handType, isVisible);
        }

        [ClientRpc]
        private void RpcVisualToggleHand(HandType handType,  bool isVisible)
        {
            HandData hand = handType == HandType.Left ? _leftHandData : _rightHandData;
            hand.ToggleHandVisible(isVisible);
        }
        
        [Command(requiresAuthority = false)]
        public void CmdChangeNetHandDataType(HandType handType, HandDataType handDataType)
        {
            RpcChangeNetHandDataType(handType, handDataType);
        }

        [ClientRpc]
        private void RpcChangeNetHandDataType(HandType handType, HandDataType handDataType)
        {
            HandData hand = handType == HandType.Left ? _leftHandData : _rightHandData;
            hand.UpdateHandDataType(handDataType);
        }

        [Command(requiresAuthority = false)]
        private void CmdSyncVisualHand()
        {
            RpcChangeNetHandDataType(HandType.Left, _leftHandData.HandDataType);
            RpcChangeNetHandDataType(HandType.Right, _rightHandData.HandDataType);
        }
    }
}