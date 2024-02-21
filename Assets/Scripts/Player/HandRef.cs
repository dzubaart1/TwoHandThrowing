using UnityEngine;

namespace Player
{
    public class HandRef : MonoBehaviour
    {
        public HandData HandData => _handData;

        [SerializeField] private HandData _handData;
    }
}
