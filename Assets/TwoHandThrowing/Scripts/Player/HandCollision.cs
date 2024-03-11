using TwoHandThrowing.Core;
using TwoHandThrowing.Gameplay;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace TwoHandThrowing.Player
{
    [RequireComponent(typeof(Rigidbody), typeof(ActionBasedController), typeof(XRDirectInteractor))]
    public class HandCollision : MonoBehaviour
    {
        public float MaxVelocityToAttach = 1;
        
        public HandData HandData { get; private set; }
        public XRDirectInteractor Interactor { get; private set; }
        public ActionBasedController Controller { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        
        [SerializeField] private Collider[] _handColliders;
        [SerializeField] private HandData _handData;
        
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Interactor = GetComponent<XRDirectInteractor>();
            Controller = GetComponent<ActionBasedController>();

            HandData = _handData;
        }

        [ContextMenu("Config Hand Collisions")]
        private void CollectCollider()
        {
            _handColliders = gameObject.GetComponentsInChildren<Collider>();
        }

        public void TogglePhysics(bool isEnable)
        {
            foreach (var collider in _handColliders)
            {
                collider.enabled = isEnable;
            }

            if (isEnable)
            {
                Rigidbody.WakeUp();
            }
            else
            {
                Rigidbody.Sleep();
            }
        }

        public void UpdatePhysicMaterial(PhysicMaterial physicMaterial)
        {
            foreach (var collider in _handColliders)
            {
                collider.material = physicMaterial;
            }
        }
        
        private void OnCollisionExit(Collision collision)
        {
            var ball = collision.gameObject.GetComponent<Ball>();

            if (ball == null)
            {
                return;
            }

            if (!Controller.selectAction.action.inProgress |
                HandData.HandDataType != HandDataType.Goalkeeper)
            {
                return;
            }

            if (ball.Rigidbody.velocity.sqrMagnitude > MaxVelocityToAttach)
            {
                return;
            }
            
            Interactor.interactionManager.SelectEnter(Interactor, (IXRSelectInteractable)ball.HandGrabInteractable);
        }
    }
}
