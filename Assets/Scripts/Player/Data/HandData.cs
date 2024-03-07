using Assets.Scripts.Core.Services;
using TwoHandThrowing.Core;
using UnityEngine;

namespace TwoHandThrowing.Player
{
    [RequireComponent(typeof(Animator))]
    public class HandData : MonoBehaviour
    {
        [Header("Hand Config")]
        public HandType HandType = HandType.Left;
        public HandDataType HandDataType = HandDataType.Common;
        
        public Animator Animator { get; private set; }
        
        public SkinnedMeshRenderer Renderer => _renderer;
        public Transform Root => _root;
        public Transform[] Bones => _bones;
        
        [Space]
        [Header("Bones")]
        [SerializeField] private Transform _root;
        [SerializeField] private Transform[] _bones;
        [SerializeField] private SkinnedMeshRenderer _renderer;

        private HandDataConfigurationService _handDataConfigurationService;
        
        private void Awake()
        {
            Animator = GetComponent<Animator>();

            _handDataConfigurationService = Engine.GetService<HandDataConfigurationService>();
        }
        
        public void UpdateHandDataType(HandDataType handDataType)
        {
            HandDataSettings settings = _handDataConfigurationService.GetSettingsByType(handDataType);
            
            switch (HandType)
            {
                case HandType.Left:
                    Animator.runtimeAnimatorController = settings.LeftHandAnimatorController;
                    break;
                case HandType.Right:
                    Animator.runtimeAnimatorController = settings.RightHandAnimatorController;
                    break;
            }
            
            Renderer.sharedMaterial = settings.HandMaterial;
            HandDataType = settings.HandDataType;
        }
    }

    public enum HandType
    {
        Left, Right
    }
}
