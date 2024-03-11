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
        
        private HandCollision _rightHand;
        private HandCollision _leftHand;

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
            HandCollision hand = args.interactorObject.Equals(_leftHand.Interactor) ? _leftHand : _rightHand;
            Transform newAttachTransform = hand.HandData.HandType == HandType.Left
                ? _leftHandAttachTransform
                : _rightHandAttachTransform;
            
            if (interactorsSelecting.Count != 1)
            {
                attachTransform.SetPositionAndRotation(newAttachTransform.position, newAttachTransform.rotation);
            }
            
            hand.TogglePhysics(false);
            
            base.OnSelectEntering(args);
            
            SelectEnteringEvent?.Invoke(args);
        }

        protected override void OnSelectExiting(SelectExitEventArgs args)
        {
            HandCollision hand = args.interactorObject.Equals(_leftHand.Interactor) ? _leftHand : _rightHand;
            Transform newAttachTransform = hand.HandData.HandType == HandType.Left
                ? _rightHandAttachTransform
                : _leftHandAttachTransform;

            if (interactorsSelecting.Count != 1)
            {
                attachTransform.SetPositionAndRotation(newAttachTransform.position, newAttachTransform.rotation);
            }
            
            hand.TogglePhysics(true);
            
            base.OnSelectExiting(args);
        }
    }
}
