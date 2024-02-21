using UnityEngine;
using MaterialFactory.Tools;

namespace MaterialFactory.BallStuff
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private float _secondsToDestroy = 2f;

        private Rigidbody _rigidbody;

        private BallConfiguration _ballConfiguration;

        public void AddForce(Vector3 force)
        {
            _rigidbody.AddForce(force);
        }

        public void SetBallConfiguration(BallConfiguration ballConfiguration)
        {
            _ballConfiguration = ballConfiguration;
            _rigidbody = gameObject.GetComponent<Rigidbody>();

            if (_rigidbody is null)
            {
                _rigidbody = gameObject.AddComponent<Rigidbody>().GetCopyOf(ballConfiguration.RigidBody);
            }
            else
            {
                _rigidbody = gameObject.GetComponent<Rigidbody>().GetCopyOf(ballConfiguration.RigidBody);
            }

            _collider.material = _ballConfiguration.PhysicMaterial;
        }

        public void DestroyBall()
        {
            Destroy(gameObject, _secondsToDestroy);
        }
    }
}
