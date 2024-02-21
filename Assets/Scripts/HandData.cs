using UnityEngine;

namespace Assets.Scripts
{
    public class HandData : MonoBehaviour
    {
        public HandType HandType;
        public Animator Animator => _animator;
        public Transform Root => _root;
        public Transform[] Bones => _bones;

        [SerializeField] private Transform _root;
        [SerializeField] private Transform[] _bones;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
    }

    public enum HandType
    {
        Left, Right
    }
}
