using Core;
using Services;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player
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
            _rightInteractor = _inputService.LocalPlayer.RightInteractor;
            _leftInteractor = _inputService.LocalPlayer.LeftInteractor;
        }

        protected override void OnSelectEntering(SelectEnterEventArgs args)
        {
            if(args.interactorObject == _leftInteractor)
            {
                
                if(interactorsSelecting.Count == 1)
                {

                }
                else
                {
                    attachTransform.SetPositionAndRotation(_leftHandAttachTransform.position, _leftHandAttachTransform.rotation);
                }
            }
            else if (args.interactorObject == _rightInteractor)
            {
                if (interactorsSelecting.Count == 1)
                {

                }
                else
                {
                    attachTransform.SetPositionAndRotation(_rightHandAttachTransform.position, _rightHandAttachTransform.rotation);
                }
            }

            base.OnSelectEntering(args);
        }

        protected override void OnSelectExiting(SelectExitEventArgs args)
        {
            if (args.interactorObject == _leftInteractor)
            {
                if (interactorsSelecting.Count == 2)
                {
                    attachTransform.SetPositionAndRotation(_rightHandAttachTransform.position, _rightHandAttachTransform.rotation);
                }
            }
            else if (args.interactorObject == _rightInteractor)
            {
                if (interactorsSelecting.Count == 2)
                {
                    attachTransform.SetPositionAndRotation(_leftHandAttachTransform.position, _leftHandAttachTransform.rotation);
                }
            }

            base.OnSelectExiting(args);
        }
    }
}
