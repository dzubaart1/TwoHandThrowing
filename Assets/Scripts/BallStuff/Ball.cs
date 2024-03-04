using System.Collections;
using UnityEngine;
using Mirror;
using TwoHandThrowing.Player;
using TwoHandThrowing.Tools;

namespace TwoHandThrowing.BallStuff
{
    [RequireComponent(typeof(Rigidbody), typeof(HandGrabInteractable))]
    public class Ball : NetworkBehaviour
    {
        public HandGrabInteractable HandGrabInteractable { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        
        [SerializeField] private Collider _collider;

        private BallConfiguration _ballConfiguration;
        private float _lifeTime;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            HandGrabInteractable = GetComponent<HandGrabInteractable>();
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

        public void OnDestroy()
        {
            StartCoroutine(DestroyCoroutine());
        }

        private IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(_lifeTime);
            NetworkServer.Destroy(gameObject);
        }
    }
}
