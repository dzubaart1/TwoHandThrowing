using Mirror;
using TwoHandThrowing.Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;

namespace TwoHandThrowing.Core
{
    public class InputService : IService
    {
        public InputConfiguration Configuration { get; private set; }
        public LocalPlayer LocalPlayer { get; private set; }

        public InputService(InputConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Initialize()
        {
            var obj = new GameObject("InputManager", typeof(EventSystem));

            obj.AddComponent<XRUIInputModule>();
            obj.AddComponent<XRInteractionManager>();

            LocalPlayer = Object.Instantiate(Configuration.LocalPlayer, new Vector3(0, 1, 0), Quaternion.identity);
        }

        public void Destroy()
        {
        }
    }
}
