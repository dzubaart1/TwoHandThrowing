using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts
{
    public class HandGrabInteractable : XRGrabInteractable
    {
        [SerializeField] private Transform _leftHandAttachTransform;
        [SerializeField] private Transform _rightHandAttachTransform;
        [SerializeField] private XRDirectInteractor _rightInteractor;
        [SerializeField] private XRDirectInteractor _leftInteractor;

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
                Debug.Log("<color=yellow>Left</color>");

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
                Debug.Log("<color=yellow>Right</color>");
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
