using Unity.XR.CoreUtils;
using UnityEngine;

namespace Keyboard.Services
{
    public class InputService
    {
        public XROrigin Player { get; private set; }

        private const string XR_ORIGIN_PATH = "Prefabs/XROrigin";

        private XROrigin _xrOriginPrefab;

        private static InputService _singleton;
        public static InputService Instance()
        {
            return _singleton ??= new InputService();
        }

        public InputService()
        {
            _xrOriginPrefab = Resources.Load<XROrigin>(XR_ORIGIN_PATH);
            Player = Object.Instantiate(_xrOriginPrefab);
        }
    }
}
