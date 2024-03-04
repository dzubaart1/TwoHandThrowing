using UnityEngine;

namespace TwoHandThrowing.Player
{
    public class GhostHandData : MonoBehaviour
    {
        public HandType HandType => _handType;
        public Transform Root => _root;
        public SkinnedMeshRenderer Renderer => _renderer;

        [SerializeField] private HandType _handType;
        [SerializeField] private Transform _root;
        [SerializeField] private SkinnedMeshRenderer _renderer;
    }
}

