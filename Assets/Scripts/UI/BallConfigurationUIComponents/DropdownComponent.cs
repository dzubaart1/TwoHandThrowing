using UnityEngine;
using TMPro;
using System;

namespace MaterialFactory
{

    [RequireComponent(typeof(TMP_Dropdown))]
    public class DropdownComponent : MonoBehaviour
    {
        private TMP_Dropdown _dropDown;

        private void Awake()
        {
            _dropDown = GetComponent<TMP_Dropdown>();
        }

        public T GetDropDownEnumValue<T>() where T : Enum
        {
            string value = _dropDown.options[_dropDown.value].text;
            return (T)Enum.Parse(typeof(T), value);
        }
    }
}