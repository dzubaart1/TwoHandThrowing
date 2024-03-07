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
        [SerializeField] private GhostHandData _ghostHandData;

        private SkinnedMeshRenderer _handVisual;

        private InputService _inputService;

        private void Awake()
        {
            _inputService = Engine.GetService<InputService>();
        }

        private void Start()
        {
            switch(_ghostHandData.HandType)
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
            GameObject point = Instantiate(_attachPointPrefab, transform);
            point.transform.localPosition = transform.InverseTransformPoint(_wrist.position);
        }

        private void OnSelectEntered(SelectEnterEventArgs args)
        {
            HandCollision handCollision = args.interactorObject.transform.GetComponent<HandCollision>();
            if (handCollision is null || handCollision.HandData.HandType != _ghostHandData.HandType)
            {
                return;
            }
            
            _handVisual.enabled = false;
            _ghostHandData.Renderer.enabled = true;
        }

        private void OnSelectExited(SelectExitEventArgs args)
        {
            HandCollision handCollision = args.interactorObject.transform.GetComponent<HandCollision>();
            if (handCollision is null || handCollision.HandData.HandType != _ghostHandData.HandType)
            {
                return;
            }

            _handVisual.enabled = true;
            _ghostHandData.Renderer.enabled = false;
        }
    }
}