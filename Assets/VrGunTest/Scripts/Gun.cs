using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

namespace VrGunTest.Scripts
{
    public class Gun : MonoBehaviour
    {
        [Header("Sliders")]
        [SerializeField] private Slider _rateSlider;
        [SerializeField] private Slider _lifeTimeSlider;
        [SerializeField] private Slider _forceSlider;
        [Space]
        [Header("Buttons")]
        [SerializeField] private Button _startStopButton;
        [Space]
        [Header("Spawning")]
        [SerializeField] private Rigidbody _ballPrefab;
        [SerializeField] private Transform _ballSpawnPoint;
        [Space]
        [Header("Other")]
        [SerializeField] private XRGrabInteractable _xrGrabInteractable;
        
        private bool _isStarted;

        private float _shootDelay;
        
        private float _timeToShot;
        
        private void OnEnable()
        {
            _startStopButton.onClick.AddListener(OnStartStopButtonClick);
        }

        private void OnDisable()
        {
            _startStopButton.onClick.RemoveListener(OnStartStopButtonClick);
        }

        private void OnStartStopButtonClick()
        {
            _isStarted = !_isStarted;
            if (_isStarted)
            {
                _timeToShot = _shootDelay;
                _shootDelay = 1f / (_rateSlider.value/60);
            }
            ChangeInteractableSettingsAndObject(!_isStarted);
        }

        private void ChangeInteractableSettingsAndObject(bool isInteractable)
        {
            _rateSlider.interactable = isInteractable;
            _lifeTimeSlider.interactable = isInteractable;
            _forceSlider.interactable = isInteractable;
            _xrGrabInteractable.enabled = isInteractable;
        }

        private void Update()
        {
            if (_isStarted)
                PeriodicalShot();
        }

        private void PeriodicalShot()
        {
            if (_timeToShot >= _shootDelay)
            {
                _timeToShot = 0;
                Shot();
            }

            _timeToShot += Time.deltaTime;
        }

        private void Shot()
        {
            Rigidbody ball = Instantiate(_ballPrefab, _ballSpawnPoint.position, Quaternion.identity);
            ball.AddForce(_forceSlider.value * _ballSpawnPoint.transform.forward, ForceMode.Force);

            Destroy(ball, _lifeTimeSlider.value);
        }
    }
}
