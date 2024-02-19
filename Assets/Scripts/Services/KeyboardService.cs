using Keyboard.UI;
using UnityEngine;

namespace Keyboard.Services
{
    public class KeyboardService
    {
        public KeyboardComponent KeyBoardComp { get; private set; }

        private const string KEYBOARD_PATH = "Prefabs/Keyboard/Keyboard";

        private KeyboardComponent _keyboardPrefab;

        private static KeyboardService _singleton;
        public static KeyboardService Instance()
        {
            return _singleton ??= new KeyboardService();
        }

        ~KeyboardService()
        {
            TextFieldComponent.SelectTextFieldEvent -= EnableKeyboard;
        }

        public void SpawnKeyboard()
        {
            _keyboardPrefab = Resources.Load<KeyboardComponent>(KEYBOARD_PATH);
            KeyBoardComp = Object.Instantiate(_keyboardPrefab);
            KeyBoardComp.gameObject.SetActive(false);
            KeyBoardComp.transform.SetParent(InputService.Instance().Player.transform);

            TextFieldComponent.SelectTextFieldEvent += EnableKeyboard;
        }

        public void EnableKeyboard(TextFieldComponent component)
        {
            KeyBoardComp.gameObject.SetActive(true);
        }
    }
}
