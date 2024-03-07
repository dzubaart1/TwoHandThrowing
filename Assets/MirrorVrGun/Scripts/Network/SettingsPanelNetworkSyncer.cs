using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace VrGunTest.Scripts.Network
{
    public class SettingsPanelNetworkSyncer : NetworkBehaviour
    {
        [SerializeField] private Slider _rateSlider;
        [SerializeField] private Slider _lifeTimeSlider;
        [SerializeField] private Slider _forceSlider;

        private void OnEnable()
        {
            _rateSlider.onValueChanged.AddListener(OnRateSliderValueChanged);
            _lifeTimeSlider.onValueChanged.AddListener(OnLifeTimeSliderValueChanged);
            _forceSlider.onValueChanged.AddListener(OnForceSliderValueChanged);
        }

        private void OnDisable()
        {
            _rateSlider.onValueChanged.RemoveListener(OnRateSliderValueChanged);
            _lifeTimeSlider.onValueChanged.RemoveListener(OnLifeTimeSliderValueChanged);
            _forceSlider.onValueChanged.RemoveListener(OnForceSliderValueChanged);
        }

        private void OnRateSliderValueChanged(float rate)
        {
            CmdSyncRate(rate);
        }
        
        [Command(requiresAuthority = false)]
        private void CmdSyncRate(float rate)
        {
            _rateSlider.SetValueWithoutNotify(rate);
            RpcSyncRate(rate);
        }
        
        private void RpcSyncRate(float rate)
        {
            _rateSlider.SetValueWithoutNotify(rate);
        }
        
        private void OnLifeTimeSliderValueChanged(float lifeTime)
        {
            CmdSyncLifeTime(lifeTime);
        }
        
        [Command(requiresAuthority = false)]
        private void CmdSyncLifeTime(float lifeTime)
        {
            _lifeTimeSlider.SetValueWithoutNotify(lifeTime);
            RpcSyncLifeTime(lifeTime);
        }
        
        private void RpcSyncLifeTime(float lifeTime)
        {
            _lifeTimeSlider.SetValueWithoutNotify(lifeTime);
        }
        
        private void OnForceSliderValueChanged(float force)
        {
            CmdSyncForce(force);
        }
        
        [Command(requiresAuthority = false)]
        private void CmdSyncForce(float force)
        {
            _forceSlider.SetValueWithoutNotify(force);
            RpcSyncForce(force);
        }
        
        private void RpcSyncForce(float force)
        {
            _forceSlider.SetValueWithoutNotify(force);
        }
    }
}