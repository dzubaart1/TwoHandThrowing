using JetBrains.Annotations;
using TwoHandThrowing.Core;
using TwoHandThrowing.Player;
using UnityEngine;
using NetworkPlayer = TwoHandThrowing.Network.NetworkPlayer;

namespace TwoHandThrowing.Gameplay
{
    public class HandChanger : MonoBehaviour
    {
        [SerializeField] private HandDataType _handDataType;

        private NetworkService _networkService;

        private void Awake()
        {
            _networkService = Engine.GetService<NetworkService>();
        }

        private void OnTriggerEnter(Collider col)
        {
            HandCollision handCollision = FindComponentInParents<HandCollision>(col.transform);
            ;

            if (handCollision == null)
            {
                return;
            }

            handCollision.HandData.UpdateHandDataType(_handDataType);
            UpdateNetHandDataSettings(handCollision.HandData.HandType, _handDataType);
        }

        private void UpdateNetHandDataSettings(HandType handType, HandDataType handDataType)
        {
            NetworkPlayer player = _networkService.CurrentNetworkPlayer;
            if (player == null)
            {
                return;
            }

            player.CmdChangeNetHandDataType(handType, handDataType);
        }

        [CanBeNull]
        private TComp FindComponentInParents<TComp>(Transform obj) where TComp : Component
        {
            if (obj == null)
            {
                return null;
            }

            TComp comp = obj.GetComponentInParent<TComp>();

            return comp ? comp : FindComponentInParents<TComp>(obj.parent);
        }
    }
}