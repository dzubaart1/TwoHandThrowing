using UnityEngine;
using MaterialFactory.Tools;
using Mirror;

namespace TwoHandThrowing.BallStuff
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : NetworkBehaviour
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private float _secondsToDestroy = 2f;

        private Rigidbody _rigidbody;

        private BallConfiguration _ballConfiguration;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
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
        }

        public void DestroyBall()
        {
            Destroy(gameObject, _secondsToDestroy);
        }
    }
}
