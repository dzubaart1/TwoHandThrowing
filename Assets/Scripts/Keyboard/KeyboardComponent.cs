using MaterialFactory;
using TMPro;
using UnityEngine;

namespace Keyboard
{
    public class KeyboardComponent : MonoBehaviour
    {
        private TextFieldComponent _currentTextField;
        private TMP_InputField _currentInputField;

        public void Awake()
        {
            TextFieldComponent.SelectTextFieldEvent += OnAnyTextFieldSelect;
        }

        public void KeyPressed(Key key)
        {
            if (_currentTextField is null)
            {
                return;
            }

            switch(key.Value)
            {
                case "Backspace":
                    Backspace();
                    break;
                case "Enter":
                    Enter();
                    break;
                default:
                    int m_CaretPosition = _currentTextField.InputField.caretPosition;

                    _currentTextField.InputField.text = _currentTextField.InputField.text.Insert(m_CaretPosition, key.Value);
                    m_CaretPosition++;
                    UpdateCaretPosition(m_CaretPosition);
                    break;
            }
        }

        public void Enter()
        {
            gameObject.SetActive(false);
        }

        public void Backspace()
        {
            // check if text is selected
            int caretPosition;

            if (_currentInputField.selectionFocusPosition != _currentInputField.caretPosition ||
                _currentInputField.selectionAnchorPosition != _currentInputField.caretPosition)
            {
                if (_currentInputField.selectionAnchorPosition > _currentInputField.selectionFocusPosition) // right to left
                {
                    _currentInputField.text = _currentInputField.text.Substring(0, _currentInputField.selectionFocusPosition) + _currentInputField.text.Substring(_currentInputField.selectionAnchorPosition);
                    _currentInputField.caretPosition = _currentInputField.selectionFocusPosition;
                }
                else // left to right
                {
                    _currentInputField.text = _currentInputField.text.Substring(0, _currentInputField.selectionAnchorPosition) + _currentInputField.text.Substring(_currentInputField.selectionFocusPosition);
                    _currentInputField.caretPosition = _currentInputField.selectionAnchorPosition;
                }

                caretPosition = _currentInputField.caretPosition;
                _currentInputField.selectionAnchorPosition = caretPosition;
                _currentInputField.selectionFocusPosition = caretPosition;
            }
            else
            {
                caretPosition = _currentInputField.caretPosition;

                if (caretPosition > 0)
                {
                    --caretPosition;
                    _currentInputField.text = _currentInputField.text.Remove(caretPosition, 1);
                    UpdateCaretPosition(caretPosition);
                }
            }
        }

        private void UpdateCaretPosition(int newPos)
        {
            _currentInputField.caretPosition = newPos;
        }

        private void OnDestroy()
        {
            TextFieldComponent.SelectTextFieldEvent -= OnAnyTextFieldSelect;
        }

        private void OnAnyTextFieldSelect(TextFieldComponent textFieldComponent)
        {
            _currentTextField = textFieldComponent;
            _currentInputField = textFieldComponent.InputField;
            textFieldComponent.InputField.ActivateInputField();
        }
    }
}
