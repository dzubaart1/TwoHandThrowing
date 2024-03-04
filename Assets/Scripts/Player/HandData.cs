using TwoHandThrowing.BallStuff;
using TwoHandThrowing.Core;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace TwoHandThrowing.Player
{
    public class HandData : MonoBehaviour
    {
        public HandType HandType = HandType.Left;
        public float MaxVelocityToAttach = 1;
        public HandDataType HandDataType { get; private set; }
        public Animator Animator { get; private set; }

        public Rigidbody Rigidbody => _rigidbody;
        public SkinnedMeshRenderer Renderer => _renderer;
        public Transform Root => _root;
        public Transform[] Bones => _bones;

        [SerializeField] private Transform _root;
        [SerializeField] private Transform[] _bones;
        [SerializeField] private SkinnedMeshRenderer _renderer;
        [SerializeField] private Rigidbody _rigidbody;

        private InputService _inputService;
        
        private void Awake()
        {
            Animator = GetComponent<Animator>();

            _inputService = Engine.GetService<InputService>();
        }

        public void MapTransformWith(HandData fromHand)
        {
            MapTransform(fromHand.Root, Root);
            MapTransform(fromHand.Root.parent, Root.parent);
        }

        private void MapTransform(Transform from, Transform to)
        {
            to.rotation = from.rotation;
            to.position = from.position;
        }
        
        public void UpdateHandDataSettings(HandDataSettings settings)
        {
            switch (HandType)
            {
                case HandType.Left:
                    Animator.runtimeAnimatorController = settings.LeftHandAnimatorController;
                    break;
                case HandType.Right:
                    Animator.runtimeAnimatorController = settings.RightHandAnimatorController;
                    break;
            }
            
            Renderer.material = settings.HandMaterial;
            HandDataType = settings.HandDataType;
        }

        public void TryGrabBall(Ball ball)
        {
            HandRef handRef = _inputService.LocalPlayer.LeftHand;
            
            if (HandType is HandType.Right)
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
            
            if (ball.Rigidbody.velocity.sqrMagnitude > MaxVelocityToAttach)
            {
                return;
            }
            
            handRef.Interactor.interactionManager.SelectEnter(handRef.Interactor, (IXRSelectInteractable)ball.HandGrabInteractable);
        }
    }

    public enum HandType
    {
        Left, Right
    }
}
