using Mirror;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace VrGunTest.Scripts.Network
{
    public class ChangeOwnerGrabbableObject : NetworkBehaviour
    {
        [SerializeField] private XRGrabInteractable _xrGrabInteractable;

        public override void OnStartClient()
        {
            base.OnStartClient();
            
            _xrGrabInteractable.firstSelectEntered.AddListener(OnFirstSelectEntered);
            _xrGrabInteractable.lastSelectExited.AddListener(OnLastSelectExited);
        }

        public override void OnStopClient()
        {
            base.OnStopClient();
            
            _xrGrabInteractable.firstSelectEntered.RemoveListener(OnFirstSelectEntered);
            _xrGrabInteractable.lastSelectExited.RemoveListener(OnLastSelectExited);
        }

        public void OnFirstSelectEntered(SelectEnterEventArgs args)
        {
            CmdPickup(NetworkServer.localConnection);
        }

        private void OnLastSelectExited(SelectExitEventArgs arg0)
        {
            CmdPickup();
        }

        [Command(requiresAuthority = false)]
        public void CmdPickup(NetworkConnectionToClient sender = null)
        {
            if (sender == null)
            {
                netIdentity.RemoveClientAuthority();
                RpcPickup(null);

            }
            else if (sender != netIdentity.connectionToClient)
            {
                Debug.Log("Mirror CmdPickup owner set to: " + sender.identity);

                netIdentity.RemoveClientAuthority();
                netIdentity.AssignClientAuthority(sender);
                RpcPickup(sender.identity);
            }
        }

        // Почему то миррор не сразу обновляет информацию о владельце объекта по этому приходится передавать нового владельца и сверять с собой
        [ClientRpc(includeOwner = true)]
        private void RpcPickup(NetworkIdentity newOwner)
        {
            //_xrGrabInteractable.enabled = newOwner != NetworkServer.localConnection.identity.;
        }
    }
}