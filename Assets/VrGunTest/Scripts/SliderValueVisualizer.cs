using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VrGunTest.Scripts
{
    public class SliderValueVisualizer : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _valueText;

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
            _valueText.text = value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
