using System;
using UnityEngine;

namespace TwoHandThrowing.Player
{
    public class GhostHandData : MonoBehaviour
    {
        public HandType HandType { get; private set; }
        public Transform Root { get; private set; }
        public SkinnedMeshRenderer Renderer { get; private set; }

        [SerializeField] private HandType _handType;
        [SerializeField] private Transform _root;
        [SerializeField] private SkinnedMeshRenderer _renderer;

        private void Awake()
        {
            HandType = _handType;
            Root = _root;
            Renderer = _renderer;
        }
    }
}

