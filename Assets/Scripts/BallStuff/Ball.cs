using UnityEngine;
using MaterialFactory.Tools;
using Mirror;
using TwoHandThrowing.Player;

namespace TwoHandThrowing.BallStuff
{
    [RequireComponent(typeof(Rigidbody), typeof(HandGrabInteractable))]
    public class Ball : NetworkBehaviour
    {
        [SerializeField] private Collider _collider;

        private Rigidbody _rigidbody;
        private HandGrabInteractable _handGrabInteractable;

        private BallConfiguration _ballConfiguration;
        private float _lifeTime;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _handGrabInteractable = GetComponent<HandGrabInteractable>();
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

            _handGrabInteractable.throwSmoothingDuration = ballConfiguration.SmoothingDuration;
            _handGrabInteractable.throwVelocityScale = ballConfiguration.VelocityScale;
            _handGrabInteractable.throwAngularVelocityScale = ballConfiguration.AngularVelocityScale;

            _lifeTime = ballConfiguration.LifeTime;
        }

        public void DestroyBall()
        {
            Destroy(gameObject, _lifeTime);
        }
    }
}
