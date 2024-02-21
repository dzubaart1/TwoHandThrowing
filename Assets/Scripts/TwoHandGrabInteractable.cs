using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts
{
    public class TwoHandGrabInteractable : XRGrabInteractable
    {
        public List<XRSimpleInteractable> secondHandGrabPoints = new List<XRSimpleInteractable>();
        public enum TwoHandRotationType { None, First, Second}
        public TwoHandRotationType twoHandRotation;
        public bool SnapToSecondHand = true;
        private Quaternion initialRotationOffset;

        private XRBaseInteractor secondInteractor;
        private Quaternion attachInitialRotation;

        private void Start()
        {
            foreach(var item in secondHandGrabPoints)
            {
                item.onSelectEntered.AddListener(OnSecondHandGrab);
                item.onSelectExited.AddListener(OnSecondHandRelease);
            }
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            if(secondInteractor && selectingInteractor)
            {
                if(SnapToSecondHand)
                    selectingInteractor.attachTransform.rotation = GetTwoHandRotation();
                else
                    selectingInteractor.attachTransform.rotation = GetTwoHandRotation() * initialRotationOffset;
            }

            base.ProcessInteractable(updatePhase);
        }

        public void OnSecondHandGrab(XRBaseInteractor interactor)
        {
            secondInteractor = interactor;
            initialRotationOffset = Quaternion.Inverse(GetTwoHandRotation()) * selectingInteractor.attachTransform.rotation;
        }

        public void OnSecondHandRelease(XRBaseInteractor interactor)
        {
            secondInteractor = null;
        }

        protected override void OnSelectEntered(XRBaseInteractor interactor)
        {
            base.OnSelectEntered(interactor);
            attachInitialRotation = interactor.attachTransform.localRotation;
        }

        protected override void OnSelectExited(XRBaseInteractor interactor)
        {
            base.OnSelectExited(interactor);
            secondInteractor = null;
            interactor.attachTransform.localRotation = attachInitialRotation;
        }

        public override bool IsSelectableBy(XRBaseInteractor interactor)
        {
            bool isAlreadyGrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);
            return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
        }

        private Quaternion GetTwoHandRotation()
        {
            Quaternion targetRotation = Quaternion.identity;

            switch(twoHandRotation)
            {
                case TwoHandRotationType.None:
                    targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position);
                    break;
                case TwoHandRotationType.First:
                    targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, selectingInteractor.attachTransform.up);
                    break;
                case TwoHandRotationType.Second:
                    targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, secondInteractor.attachTransform.up);
                    break;
            }

            return targetRotation;
        }
    }
}
