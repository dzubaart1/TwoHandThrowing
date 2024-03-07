using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace VrGunTest.Scripts.Network
{
    public class NetworkSlider : NetworkBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private SliderValueVisualizer _valueVisualizer;

        [SyncVar(hook = nameof(ValueChangedHook))] private float _value;

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }
        
        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }
        private void OnSliderValueChanged(float value)
        {
            _slider.SetValueWithoutNotify(value);
            CmdChangeValue(value);
        }

        [Command(requiresAuthority = false)]
        private void CmdChangeValue(float value)
        {
            _value = value;
        }

        private void ValueChangedHook(float oldValue, float newValue)
        {
            _slider.SetValueWithoutNotify(newValue);
            _valueVisualizer.SetValue(newValue);
        }
    }
}