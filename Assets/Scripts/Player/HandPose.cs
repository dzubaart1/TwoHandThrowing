using Services;
using Core;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player
{
    public class HandPose : MonoBehaviour
    {
        [SerializeField] private GameObject _attachPointPrefab;
        [SerializeField] private Transform _wrist;
        [SerializeField] private HandGrabInteractable _handGrabInteractable;
        [SerializeField] private HandData _handDataPose;


        [SerializeField] private GameObject _leftHandGhostVisual;
        [SerializeField] private GameObject _rightHandGhostVisual;

        private GameObject _leftHandVisual;
        private GameObject _rightHandVisual;

        private InputService _inputService;

        private void Awake()
        {
            _inputService = Engine.GetService<InputService>();
        }

        private void Start()
        {
            _leftHandVisual = _inputService.LocalPlayer.LeftHandVisual;
            _rightHandVisual = _inputService.LocalPlayer.RightHandVisual;

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

            switch (handRef.HandData.HandType)
            {
                case HandType.Left:
                    _leftHandVisual.SetActive(false);
                    _leftHandGhostVisual.SetActive(true);
                    break;
                case HandType.Right:
                    _rightHandVisual.SetActive(false);
                    _rightHandGhostVisual.SetActive(true);
                    break;
            }
        }

        private void OnSelectExited(SelectExitEventArgs args)
        {
            var handRef = args.interactorObject.transform.GetComponent<HandRef>();
            if (handRef is null || handRef.HandData.HandType != _handDataPose.HandType)
            {
                return;
            }

            switch (handRef.HandData.HandType)
            {
                case HandType.Left:
                    _leftHandVisual.SetActive(true);
                    _leftHandGhostVisual.SetActive(false);
                    break;
                case HandType.Right:
                    _rightHandVisual.SetActive(true);
                    _rightHandGhostVisual.SetActive(false);
                    break;
            }
        }
    }
}