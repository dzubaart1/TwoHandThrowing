using TwoHandThrowing.Core;
using UnityEngine;

namespace TwoHandThrowing.Player
{
    public class HandData : MonoBehaviour
    {
        public HandType HandType;
        
        public HandDataType HandDataType { get; private set; }
        public SkinnedMeshRenderer Renderer => _renderer;
        public Animator Animator => _animator;
        public Transform Root => _root;
        public Transform[] Bones => _bones;

        [SerializeField] private Transform _root;
        [SerializeField] private Transform[] _bones;
        [SerializeField] private SkinnedMeshRenderer _renderer;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
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
    }

    public enum HandType
    {
        Left, Right
    }
}
