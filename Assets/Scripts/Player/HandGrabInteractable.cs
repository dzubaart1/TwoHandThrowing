using TwoHandThrowing.Core;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace TwoHandThrowing.Player
{
    public class HandGrabInteractable : XRGrabInteractable
    {
        [SerializeField] private Transform _leftHandAttachTransform;
        [SerializeField] private Transform _rightHandAttachTransform;
        
        private XRDirectInteractor _rightInteractor;
        private XRDirectInteractor _leftInteractor;

        private InputService _inputService;

        protected override void Awake()
        {
            _inputService = Engine.GetService<InputService>();

            base.Awake();
        }

        private void Start()
        {
            _rightInteractor = _inputService.LocalPlayer.RightHand.XRDirectInteractor;
            _leftInteractor = _inputService.LocalPlayer.LeftHand.XRDirectInteractor;
        }

        protected override void OnSelectEntering(SelectEnterEventArgs args)
        {
            if(interactorsSelecting.Count == 1)
            {
                base.OnSelectEntering(args);
                return;
            }

            if (args.interactorObject.Equals(_leftInteractor))
            {
                attachTransform.SetPositionAndRotation(_leftHandAttachTransform.position, _leftHandAttachTransform.rotation);
            }

            if(args.interactorObject.Equals(_rightInteractor))
            {
                attachTransform.SetPositionAndRotation(_rightHandAttachTransform.position, _rightHandAttachTransform.rotation);
            }

            base.OnSelectEntering(args);
        }

        protected override void OnSelectExiting(SelectExitEventArgs args)
        {
            if(interactorsSelecting.Count == 1)
            {
                base.OnSelectExiting(args);
                return;
            }

            if (args.interactorObject.Equals(_leftInteractor))
            {
                attachTransform.SetPositionAndRotation(_rightHandAttachTransform.position, _rightHandAttachTransform.rotation);
            }

            if (args.interactorObject.Equals(_rightInteractor))
            {
                attachTransform.SetPositionAndRotation(_leftHandAttachTransform.position, _leftHandAttachTransform.rotation);
            }

            base.OnSelectExiting(args);
        }
    }
}
