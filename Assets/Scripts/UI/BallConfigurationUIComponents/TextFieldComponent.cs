using System;
using TMPro;
using UnityEngine;

namespace Keyboard.UI
{
    [RequireComponent(typeof(TMP_InputField))]
    public class TextFieldComponent : MonoBehaviour
    {
        public static event Action<TextFieldComponent> SelectTextFieldEvent;

        public TMP_InputField InputField => _inputField;
        public string Text => _inputField.text;

        [SerializeField] private float _defaultValue;

        private TMP_InputField _inputField;
        
        private void Awake()
        {
            _inputField = GetComponent<TMP_InputField>();
            _inputField.onEndEdit.AddListener(CheckInput);
            _inputField.onSelect.AddListener(OnSelectText);

            _inputField.text = _defaultValue.ToString();
        }

        private void Start()
        {
            _inputField.text = _defaultValue.ToString();
        }

        private void OnDestroy()
        {
            _inputField.onEndEdit.RemoveListener(CheckInput);
            _inputField.onSelect.RemoveListener(OnSelectText);
        }

        private void CheckInput(string inputValue)
        {
            if(!float.TryParse(inputValue, out _))
            {
                _inputField.text = _defaultValue.ToString();
            }
        }
        
        private void OnSelectText(string value)
        {
            SelectTextFieldEvent?.Invoke(this);
        }
    }
}
