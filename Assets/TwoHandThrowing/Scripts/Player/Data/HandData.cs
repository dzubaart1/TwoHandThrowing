using Assets.Scripts.Core.Services;
using TwoHandThrowing.Core;
using UnityEngine;

namespace TwoHandThrowing.Player
{
    [RequireComponent(typeof(Animator))]
    public class HandData : MonoBehaviour
    {
        [Header("Hand Config")]
        public HandType HandType;
        public HandDataType HandDataType;
        
        [Space]
        [Header("Bones")]
        [SerializeField] private Transform _root;
        [SerializeField] private Transform[] _bones;
        [SerializeField] private SkinnedMeshRenderer _renderer;
        
        public Animator Animator { get; private set; }
        public Transform Root { get; private set; }
        public Transform[] Bones { get; private set; }

        private HandDataConfigurationService _handDataConfigurationService;
        
        private void Awake()
        {
            Animator = GetComponent<Animator>();

            _handDataConfigurationService = Engine.GetService<HandDataConfigurationService>();

            Root = _root;
            Bones = _bones;
        }
        
        public void UpdateHandDataType(HandDataType handDataType)
        {
            HandDataSettings settings = _handDataConfigurationService.GetSettingsByType(handDataType);

            Animator.runtimeAnimatorController = HandType == HandType.Left
                ? settings.LeftHandAnimatorController
                : settings.RightHandAnimatorController;

            _renderer.sharedMaterial = settings.HandMaterial;
            HandDataType = settings.HandDataType;
        }

        public void ToggleHandVisible(bool isVisible)
        {
            _renderer.enabled = isVisible;
        }
    }

    public enum HandType : byte
    {
        Left, Right
    }
}
