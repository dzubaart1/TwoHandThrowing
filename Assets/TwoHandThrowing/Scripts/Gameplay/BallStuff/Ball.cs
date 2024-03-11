using System.Collections;
using UnityEngine;
using Mirror;
using TwoHandThrowing.Core;
using TwoHandThrowing.Player;
using TwoHandThrowing.Tools;
using UnityEngine.XR.Interaction.Toolkit;

namespace TwoHandThrowing.Gameplay
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

        public void DestroyBall()
        {
            StartCoroutine(DestroyCoroutine());
        }

        private IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(_lifeTime);
            NetworkServer.Destroy(gameObject);
        }

        private void OnSelectEnter(SelectEnterEventArgs args)
        {
            HandCollision handCollision = args.interactorObject.transform.GetComponent<HandCollision>();

            if (handCollision is null)
            {
                return;
            }
            
            // Выключаем видимость рук у локального плеера и у клиентов
            _networkService.CurrentNetworkPlayer.CmdVisualToggleHand(handCollision.HandData.HandType, false);
            _inputService.LocalPlayer.ToggleHandVisual(handCollision.HandData.HandType, false);
            
            // Включаем видимость Hand Pose у клиентов
            CmdVisualToggleHandPose(handCollision.HandData.HandType, true);
        }
        
        private void OnSelectExit(SelectExitEventArgs args)
        {
            HandCollision handCollision = args.interactorObject.transform.GetComponent<HandCollision>();
            
            if (handCollision is null)
            {
                return;
            }
            
            // Включаем видимость рук у локального плеера и у клиентов
            _networkService.CurrentNetworkPlayer.CmdVisualToggleHand(handCollision.HandData.HandType, true);
            _inputService.LocalPlayer.ToggleHandVisual(handCollision.HandData.HandType, true);
            
            // Выключаем видимость Hand Pose у клиентов
            CmdVisualToggleHandPose(handCollision.HandData.HandType, false);
        }

        [Command(requiresAuthority = false)]
        public void CmdVisualToggleHandPose(HandType handType, bool isVisible)
        {
            RpcToggleHandPoseVisible(handType, isVisible);
        }

        [ClientRpc]
        private void RpcToggleHandPoseVisible(HandType handType, bool isVisible)
        {
            HandPose handPose = handType == HandType.Left ? _leftHandPose : _rightHandPose;
            handPose.ToggleGhostHandVisible(isVisible);
        }
    }
}
