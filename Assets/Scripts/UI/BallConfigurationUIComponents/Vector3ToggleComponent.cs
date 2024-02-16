using UnityEngine;
using UnityEngine.UI;

namespace MaterialFactory
{
    public class Vector3ToggleComponent : MonoBehaviour
    {
        [SerializeField] private Toggle _xToggle, _yToggle, _zToggle;

        private bool[] _array= new bool[3];

        public bool[] GetXYZ()
        {
            _array[0] = _xToggle.isOn;
            _array[1] = _yToggle.isOn;
            _array[2] = _zToggle.isOn;

            return _array;
        }
    }
}