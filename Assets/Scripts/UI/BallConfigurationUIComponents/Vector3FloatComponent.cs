using TMPro;
using UnityEngine;

namespace TwoHandThrowing.UI
{
    public class Vector3FloatComponent : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _xText, _yText, _zText;

        public Vector3 GetVector3()
        {
            return new Vector3(float.Parse(_xText.text), float.Parse(_yText.text), float.Parse(_zText.text));
        }
    }
}