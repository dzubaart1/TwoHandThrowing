using TwoHandThrowing.Player;
using UnityEngine;
using UnityEngine.EventSystems;
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
            GameObject inputManager = new GameObject("InputManager", new []{typeof(EventSystem), typeof(XRUIInputModule), typeof(XRInteractionManager)});

            LocalPlayer = Object.Instantiate(Configuration.LocalPlayer, new Vector3(0, 2, 0), Quaternion.identity);
        }
    }
}
