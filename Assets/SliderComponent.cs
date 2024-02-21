using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderComponent : MonoBehaviour
{
    public float Value => _slider.value;

    [SerializeField] private TextMeshProUGUI _valueText;

    [SerializeField] private Slider _slider;

    private void Awake()
    {
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    private void Start()
    {
        _valueText.text = _slider.value.ToString();
    }

    private void OnSliderValueChanged(float value)
    {
        _valueText.text = Math.Round(value,1).ToString();
    }
}
