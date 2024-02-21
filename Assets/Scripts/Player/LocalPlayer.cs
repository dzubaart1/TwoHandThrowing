using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player
{
    public class LocalPlayer : MonoBehaviour
    {
        public XRDirectInteractor LeftInteractor => _leftInteractor;
        public XRDirectInteractor RightInteractor => _rightInteractor;
        public GameObject RightHandVisual => _rightHandVisual;
        public GameObject LeftHandVisual => _leftHandVisual;

        [SerializeField] private XRDirectInteractor _leftInteractor;
        [SerializeField] private XRDirectInteractor _rightInteractor;
        [SerializeField] private GameObject _leftHandVisual;
        [SerializeField] private GameObject _rightHandVisual;
    }
}
