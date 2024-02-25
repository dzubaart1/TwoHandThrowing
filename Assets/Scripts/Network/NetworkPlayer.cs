using Mirror;
using TwoHandThrowing.Core;
using TwoHandThrowing.Player;
using UnityEngine;

namespace TwoHandThrowing.Network
{
    public class NetworkPlayer : NetworkBehaviour
    {
        public Transform Head => _head;
        public HandData LeftHandData => _leftHandData;
        public HandData RightHandData => _rightHandData;

        [SerializeField] private Transform _head;
        [SerializeField] private HandData _leftHandData;
        [SerializeField] private HandData _rightHandData;


        public override void OnStartClient()
        {
            base.OnStartClient();

            if(isOwned)
            {
                Engine.GetService<NetworkService>().CurrentNetworkPlayer = this;
                _leftHandData.Renderer.enabled = false;
                _rightHandData.Renderer.enabled = false;
            }
        }
        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();

        }
    }
}