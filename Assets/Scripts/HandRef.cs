using UnityEngine;

namespace Assets.Scripts
{
    public class HandRef : MonoBehaviour
    {
        public HandData HandData => _handData;

        [SerializeField] private HandData _handData;
    }
}
