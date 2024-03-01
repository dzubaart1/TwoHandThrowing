using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace TwoHandThrowing.Player
{
    [RequireComponent(typeof(ActionBasedController), typeof(XRDirectInteractor))]
    public class HandRef : MonoBehaviour
    {
        public XRDirectInteractor Interactor { get; private set; }
        public ActionBasedController Controller { get; private set; }

        public HandCollision Collision => _collision;
        [SerializeField] private HandCollision _collision;
        public HandData HandData => _handData;
        [SerializeField] private HandData _handData;
        
        private void Awake()
        {
            Interactor = GetComponent<XRDirectInteractor>();
            Controller = GetComponent<ActionBasedController>();
        }
    }
}
