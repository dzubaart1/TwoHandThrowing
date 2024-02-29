using System;
using TwoHandThrowing.Core;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace TwoHandThrowing.Player
{
    public class HandGrabInteractable : XRGrabInteractable
    {
        public event Action<SelectEnterEventArgs> SelectEnteringEvent;

        [SerializeField] private Transform _leftHandAttachTransform;
        [SerializeField] private Transform _rightHandAttachTransform;
        
        private HandRef _rightHand;
        private HandRef _leftHand;

        private InputService _inputService;

        protected override void Awake()
        {
            base.Awake();

            _inputService = Engine.GetService<InputService>();
        }

        private void Start()
        {
            _rightHand = _inputService.LocalPlayer.RightHand;
            _leftHand = _inputService.LocalPlayer.LeftHand;
        }

        protected override void OnSelectEntering(SelectEnterEventArgs args)
        {
            if (args.interactorObject.Equals(_leftHand.Interactor))
            {
                if (interactorsSelecting.Count != 1)
                {
                    attachTransform.SetPositionAndRotation(_leftHandAttachTransform.position, _leftHandAttachTransform.rotation);
                }
                _leftHand.Collision.TurnOffColliders();
            }

            if(args.interactorObject.Equals(_rightHand.Interactor))
            {
                if (interactorsSelecting.Count != 1)
                {
                    attachTransform.SetPositionAndRotation(_rightHandAttachTransform.position, _rightHandAttachTransform.rotation);
                }
                _rightHand.Collision.TurnOffColliders();
            }

            base.OnSelectEntering(args);
            SelectEnteringEvent?.Invoke(args);
        }

        protected override void OnSelectExiting(SelectExitEventArgs args)
        {
            if (args.interactorObject.Equals(_leftHand.Interactor))
            {
                if (interactorsSelecting.Count != 1)
                {
                    attachTransform.SetPositionAndRotation(_rightHandAttachTransform.position, _rightHandAttachTransform.rotation);
                }
                _leftHand.Collision.TurnOnColliders();
            }

            if (args.interactorObject.Equals(_rightHand.Interactor))
            {
                if (interactorsSelecting.Count != 1)
                {
                    attachTransform.SetPositionAndRotation(_leftHandAttachTransform.position, _leftHandAttachTransform.rotation);
                }
                _rightHand.Collision.TurnOnColliders();
            }

            base.OnSelectExiting(args);
        }
    }
}
