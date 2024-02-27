using Mirror;
using TwoHandThrowing.Player;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace TwoHandThrowing.Network
{
    [RequireComponent(typeof(Rigidbody), typeof(HandGrabInteractable))]
    public class NetworkInteractable : NetworkBehaviour
    {
        private Rigidbody _rigidbody;
        private HandGrabInteractable _handGrabInteractable;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _handGrabInteractable = GetComponent<HandGrabInteractable>();

            _handGrabInteractable.SelectEnteringEvent += OnSelectEnetering;
        }

        private void OnDestroy()
        {
            _handGrabInteractable.SelectEnteringEvent -= OnSelectEnetering;
        }

        public void OnSelectEnetering(SelectEnterEventArgs args)
        {
            ResetInteractableVelocity();
        }


        [Command(requiresAuthority = false)]
        public void CmdPickup(NetworkConnectionToClient sender = null)
        {
            Debug.Log("Mirror CmdPickup owner set to: " + sender.identity);

            ResetInteractableVelocity();

            if (sender != netIdentity.connectionToClient)
            {
                netIdentity.RemoveClientAuthority();
                netIdentity.AssignClientAuthority(sender);
            }
        }

        private void ResetInteractableVelocity()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}