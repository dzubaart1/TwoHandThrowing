using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Keyboard.Services;

namespace Keyboard
{
    [RequireComponent(typeof(Button))]
    public class Key : MonoBehaviour
    {
        public string Value => _value;

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private string _value = "1";

        private KeyboardService _keyboardService;

        private Button _button;

        private void Awake()
        {
            _keyboardService = KeyboardService.Instance();

            _button = gameObject.GetComponent<Button>();
            _button.onClick.AddListener(OnKeyButtonClick);

            _text.text = _value;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnKeyButtonClick);
        }

        private void OnKeyButtonClick()
        {
            _keyboardService.KeyBoardComp.KeyPressed(this);
        }
    }
}