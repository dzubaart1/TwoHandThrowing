using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace TwoHandThrowing.Player
{
    [RequireComponent(typeof(Animator))]
    public class Hand : MonoBehaviour
    {
        public const string GRIP_PARAMETER = "Grip";
        public const string TRIGGER_PARAMETER = "Trigger";
        
        [SerializeField] private InputDeviceCharacteristics _inputDeviceCharacteristics;

        private Animator _animator;

        private InputDevice _targetDevice;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            StartCoroutine(WaitForGetDevices());
        }

        private IEnumerator WaitForGetDevices()
        {
            List<InputDevice> _devices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(_inputDeviceCharacteristics, _devices);

            while (_devices.Count == 0)
            {
                yield return null;
                InputDevices.GetDevicesWithCharacteristics(_inputDeviceCharacteristics, _devices);
            }

            _targetDevice = _devices[0];
        }

        private void Update()
        {
            UpdateHand();
        }

        private void UpdateHand()
        {
            float gripValue, triggerValue;

            if (_targetDevice.TryGetFeatureValue(CommonUsages.grip, out gripValue))
            {
                _animator.SetFloat(GRIP_PARAMETER, gripValue);
            }

            if (_targetDevice.TryGetFeatureValue(CommonUsages.trigger, out triggerValue))
            {
                _animator.SetFloat(TRIGGER_PARAMETER, triggerValue);
            }
        }
    }
}