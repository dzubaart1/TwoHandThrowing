using UnityEngine;
using UnityEngine.UI;

namespace TwoHandThrowing.UI
{
    public class ToggleGroupComponent : MonoBehaviour
    {
        [SerializeField] private Toggle[] _toggles;

        public bool[] GetBits()
        {
            bool[] array = new bool[_toggles.Length];

            for(int i = 0; i < array.Length; i++)
            {
                array[i] = _toggles[i].isOn;
            }

            return array;
        }
    }
}