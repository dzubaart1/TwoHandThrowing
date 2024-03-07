using System.Collections;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using WaitForSeconds = UnityEngine.WaitForSeconds;

namespace VrGunTest.Scripts
{
    public class Gun : NetworkBehaviour
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
        [SerializeField] private ObjectCatcher _startStopTriggerZone;
        [SerializeField] private XRGrabInteractable _xrGrabInteractable;
        [SerializeField] private Rigidbody _rigidbody;
        
        
        private bool _isStarted;

        private float _shootDelay;
        
        private float _timeToShot;
        
        private void OnEnable()
        {
            _startStopButton.onClick.AddListener(CmdChangeStarted);
            _startStopTriggerZone.Caught += CmdChangeStarted;
        }

        private void OnDisable()
        {
            _startStopButton.onClick.RemoveListener(CmdChangeStarted);
            _startStopTriggerZone.Caught -= CmdChangeStarted;
        }

        [Command(requiresAuthority = false)]
        private void CmdChangeStarted()
        {
            ChangeStarted();
        }
        
        [Server]
        private void ChangeStarted()
        {
            _isStarted = !_isStarted;
            if (_isStarted)
            {
                _shootDelay = 1f / (_rateSlider.value/60);
                _timeToShot = _shootDelay;
            }
            RpcChangeInteractableSettingsAndObject(!_isStarted);
        }
        
        [ClientRpc(includeOwner = true)]
        private void RpcChangeInteractableSettingsAndObject(bool isInteractable)
        {
            _rateSlider.interactable = isInteractable;
            _lifeTimeSlider.interactable = isInteractable;
            _forceSlider.interactable = isInteractable;
            _xrGrabInteractable.enabled = isInteractable;
            _rigidbody.isKinematic = !isInteractable;
        }

        private void Update()
        {
            if(!isServer)
                return;
            
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

        [Server]
        private void Shot()
        {
            Rigidbody ball = Instantiate(_ballPrefab, _ballSpawnPoint.position, Quaternion.identity);
            NetworkServer.Spawn(ball.gameObject);
            ball.AddForce(_forceSlider.value * _ballSpawnPoint.transform.forward, ForceMode.Force);
            StartCoroutine(DestroyCoroutine(ball));
        }

        private IEnumerator DestroyCoroutine(Rigidbody ball)
        {
            yield return new WaitForSeconds(_lifeTimeSlider.value);
            
            NetworkServer.UnSpawn(ball.gameObject);
            Destroy(ball.gameObject);
        }
    }
}
