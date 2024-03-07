using System;
using UnityEngine;
using Mirror;
using TwoHandThrowing.Core;
using TwoHandThrowing.Player;
using TwoHandThrowing.Tools;
using UnityEngine.XR.Interaction.Toolkit;

namespace TwoHandThrowing.BallStuff
{
    [RequireComponent(typeof(Rigidbody), typeof(HandGrabInteractable))]
    public class Ball : NetworkBehaviour
    {
        public HandGrabInteractable HandGrabInteractable { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        
        [SerializeField] private Collider _collider;
        [SerializeField] private HandPose _rightHandPose;
        [SerializeField] private HandPose _leftHandPose;

        private BallConfiguration _ballConfiguration;
        private float _lifeTime;

        private NetworkService _networkService;
        private InputService _inputService;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            HandGrabInteractable = GetComponent<HandGrabInteractable>();

            HandGrabInteractable.selectEntered.AddListener(OnSelectEnter);
            HandGrabInteractable.selectExited.AddListener(OnSelectExit);

            _networkService = Engine.GetService<NetworkService>();
            _inputService = Engine.GetService<InputService>();
        }

        private void OnDestroy()
        {
            HandGrabInteractable.selectEntered.RemoveListener(OnSelectEnter);
            HandGrabInteractable.selectExited.RemoveListener(OnSelectExit);
        }
        

        public void AddForce(Vector3 force)
        {
            Rigidbody.AddForce(force);
        }

        public void SetBallConfiguration(BallConfiguration ballConfiguration)
        {
            _ballConfiguration = ballConfiguration;

            Rigidbody.GetCopyOf(ballConfiguration.RigidBody);
            _collider.sharedMaterial = _ballConfiguration.PhysicMaterial;

            HandGrabInteractable.throwSmoothingDuration = ballConfiguration.SmoothingDuration;
            HandGrabInteractable.throwVelocityScale = ballConfiguration.VelocityScale;
            HandGrabInteractable.throwAngularVelocityScale = ballConfiguration.AngularVelocityScale;

            _lifeTime = ballConfiguration.LifeTime;
        }

        /*public void OnDestroy()
        {
            StartCoroutine(DestroyCoroutine());
        }*/

        /*private IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(_lifeTime);
            NetworkServer.Destroy(gameObject);
        }*/

        private void OnSelectEnter(SelectEnterEventArgs args)
        {
            HandCollision handCollision = args.interactorObject.transform.GetComponent<HandCollision>();

            if (handCollision is null)
            {
                return;
            }
            
            _networkService.CurrentNetworkPlayer.CmdHideHand(handCollision.HandData.HandType);
            _inputService.LocalPlayer.HideHand(handCollision.HandData.HandType);
            CmdShowHandPose(handCollision.HandData.HandType);
        }
        
        private void OnSelectExit(SelectExitEventArgs args)
        {
            HandCollision handCollision = args.interactorObject.transform.GetComponent<HandCollision>();
            
            if (handCollision is null)
            {
                return;
            }
            
            _networkService.CurrentNetworkPlayer.CmdShowHand(handCollision.HandData.HandType);
            _inputService.LocalPlayer.ShowHand(handCollision.HandData.HandType);
            CmdHideHandPose(handCollision.HandData.HandType);
        }

        [Command]
        public void CmdShowHandPose(HandType handType)
        {
            RpcShowHandPose(handType);
        }

        [ClientRpc]
        private void RpcShowHandPose(HandType handType)
        {
            HandPose handPose = _leftHandPose;
            if (handType == HandType.Right)
            {
                handPose = _rightHandPose;
            }
            
            handPose.ShowGhostHand();
        }
        
        [Command]
        public void CmdHideHandPose(HandType handType)
        {
            RpcHideHandPose(handType);
        }

        [ClientRpc]
        private void RpcHideHandPose(HandType handType)
        {
            HandPose handPose = _leftHandPose;
            if (handType == HandType.Right)
            {
                handPose = _rightHandPose;
            }
            
            handPose.HideGhostHand();
        }
    }
}
