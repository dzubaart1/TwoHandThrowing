using System;
using System.Collections;
using UnityEngine;
using MaterialFactory.Tools;
using Mirror;
using TwoHandThrowing.Core;
using TwoHandThrowing.Player;
using UnityEngine.XR.Interaction.Toolkit;

namespace TwoHandThrowing.BallStuff
{
    [RequireComponent(typeof(Rigidbody), typeof(HandGrabInteractable))]
    public class Ball : NetworkBehaviour
    {
        public HandGrabInteractable HandGrabInteractable { get; private set; }
        
        [SerializeField] private Collider _collider;

        private InputService _inputService;
        
        private Rigidbody _rigidbody;
        
        private BallConfiguration _ballConfiguration;
        private float _lifeTime;

        private void Awake()
        {
            _inputService = Engine.GetService<InputService>();
            
            _rigidbody = GetComponent<Rigidbody>();
            HandGrabInteractable = GetComponent<HandGrabInteractable>();
        }

        public void AddForce(Vector3 force)
        {
            _rigidbody.AddForce(force);
        }

        public void SetBallConfiguration(BallConfiguration ballConfiguration)
        {
            _ballConfiguration = ballConfiguration;

            _rigidbody.GetCopyOf(ballConfiguration.RigidBody);
            _collider.material = _ballConfiguration.PhysicMaterial;

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

        private void OnCollisionExit(Collision collision)
        {
            var handData = collision.gameObject.GetComponent<HandData>();

            if (handData is null)
            {
                return;
            }

            HandRef handRef = _inputService.LocalPlayer.LeftHand;
            
            if (handData.HandType is HandType.Right)
            {
                handRef = _inputService.LocalPlayer.RightHand;
            }

            if (!handRef.Controller.selectAction.action.inProgress)
            {
                return;
            }

            if (handRef.HandData.HandDataType is not HandDataType.Goalkeeper)
            {
                return;
            }
            
            handRef.Interactor.interactionManager.SelectEnter(handRef.Interactor, (IXRSelectInteractable)HandGrabInteractable);
        }
    }
}
