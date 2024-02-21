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
                attachTransform.SetPositionAndRotation(_leftHandAttachTransform.position, _leftHandAttachTransform.rotation);
            }
            else if (args.interactorObject == _rightInteractor)
            {
                attachTransform.SetPositionAndRotation(_rightHandAttachTransform.position, _rightHandAttachTransform.rotation);
            }

            base.OnSelectEntering(args);
        }
    }
}
