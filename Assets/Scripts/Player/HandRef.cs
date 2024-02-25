using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace TwoHandThrowing.Player
{
    public class HandRef : MonoBehaviour
    {
        public XRDirectInteractor XRDirectInteractor => _xrDirectInteractor;
        public HandData HandData => _handData;

        [SerializeField] private HandData _handData;
        [SerializeField] private XRDirectInteractor _xrDirectInteractor;
    }
}
