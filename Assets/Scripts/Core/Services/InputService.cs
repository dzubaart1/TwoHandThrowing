using Player;
using System.Threading.Tasks;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace Services
{
    public class InputService : IService
    {
        public LocalPlayer LocalPlayer { get; private set; }

        private const string XR_ORIGIN_PATH = "Prefabs/LocalPlayer";

        private LocalPlayer _xrOriginPrefab;

        public Task Initialize()
        {
            _xrOriginPrefab = Resources.Load<LocalPlayer>(XR_ORIGIN_PATH);
            LocalPlayer = Object.Instantiate(_xrOriginPrefab);

            return Task.CompletedTask;
        }
    }
}
