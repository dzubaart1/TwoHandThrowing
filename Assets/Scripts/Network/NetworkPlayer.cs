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
        
        public override void OnStartClient()
        {
            base.OnStartClient();

            if(isOwned & Engine.GetService<NetworkService>().CurrentNetworkPlayer is null)
            {
                Engine.GetService<NetworkService>().SetCurrentNetworkPlayer(this);
                _leftHandData.Renderer.enabled = false;
                _rightHandData.Renderer.enabled = false;
            }
        }
    }
}