using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace TwoHandThrowing.Player
{
    [RequireComponent(typeof(Animator))]
    public class Hand : MonoBehaviour
    {
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
            if (!_targetDevice.isValid)
            {
                return;
            }

            UpdateHand();
        }

        private void UpdateHand()
        {
            float gripValue, triggerValue;

            if (_targetDevice.TryGetFeatureValue(CommonUsages.grip, out gripValue))
            {
                _animator.SetFloat("Grip", gripValue);
            }

            if (_targetDevice.TryGetFeatureValue(CommonUsages.trigger, out triggerValue))
            {
                _animator.SetFloat("Trigger", triggerValue);
            }
        }
    }
}