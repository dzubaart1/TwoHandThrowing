using TwoHandThrowing.Core;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace TwoHandThrowing.Player
{
    public class HandPose : MonoBehaviour
    {
        [SerializeField] private GameObject _attachPointPrefab;
        [SerializeField] private Transform _wrist;
        [SerializeField] private HandGrabInteractable _handGrabInteractable;
        [SerializeField] private HandData _handDataPose;

        private SkinnedMeshRenderer _handVisual;

        private InputService _inputService;

        private void Awake()
        {
            _inputService = Engine.GetService<InputService>();
        }

        private void Start()
        {
            switch(_handDataPose.HandType)
            {
                case HandType.Left:
                    _handVisual = _inputService.LocalPlayer.LeftHand.HandData.Renderer;
                    break;
                case HandType.Right:
                    _handVisual = _inputService.LocalPlayer.RightHand.HandData.Renderer;
                    break;
            }

            _handGrabInteractable.selectEntered.AddListener(OnSelectEntered);
            _handGrabInteractable.selectExited.AddListener(OnSelectExited);
        }

        private void OnDestroy()
        {
            _handGrabInteractable.selectEntered.RemoveListener(OnSelectEntered);
            _handGrabInteractable.selectExited.RemoveListener(OnSelectExited);
        }

        public void CreateAttachPoint()
        {
            var point = Instantiate(_attachPointPrefab, transform);
            point.transform.localPosition = transform.InverseTransformPoint(_wrist.position);
        }

        private void OnSelectEntered(SelectEnterEventArgs args)
        {
            var handRef = args.interactorObject.transform.GetComponent<HandRef>();
            if (handRef is null || handRef.HandData.HandType != _handDataPose.HandType)
            {
                return;
            }
            
            _handVisual.enabled = false;
            _handDataPose.Renderer.enabled = true;
            
            /*handRef.HandData.Animator.enabled = false;
            handRef.HandData.Root.parent.localRotation = _handDataPose.Root.localRotation;
            for(int i = 0; i < _handDataPose.Bones.Length; i++)
            {
                handRef.HandData.Bones[i].localRotation = _handDataPose.Bones[i].localRotation;
            }*/
        }

        private void OnSelectExited(SelectExitEventArgs args)
        {
            var handRef = args.interactorObject.transform.GetComponent<HandRef>();
            if (handRef is null || handRef.HandData.HandType != _handDataPose.HandType)
            {
                return;
            }

            _handVisual.enabled = true;
            _handDataPose.Renderer.enabled = false;

            /*handRef.HandData.Animator.enabled = true;

            if(handRef.HandData.HandType == HandType.Right)
            {
                handRef.HandData.Root.parent.localRotation = Quaternion.Euler(new Vector3(-90, 0, -90));
            }
            else if(handRef.HandData.HandType == HandType.Left)
            {
                handRef.HandData.Root.parent.localRotation = Quaternion.Euler(new Vector3(90, 0, -90));
            }*/
        }
    }
}