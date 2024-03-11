using TwoHandThrowing.Core;
using UnityEngine;
using UnityEngine.UI;

namespace TwoHandThrowing.UI
{
    public class ConnectionUI : MonoBehaviour
    {
        [SerializeField] private Button _clientBtn;
        [SerializeField] private Button _hostBtn;
        [SerializeField] private DropdownComponent _mapsDropdown;

        private NetworkService _networkService;
        private SceneSwitchingService _sceneSwitchingService;

        private void Awake()
        {
            _clientBtn.onClick.AddListener(OnClickClientBtn);
            _hostBtn.onClick.AddListener(OnClickHostBtn);

            _networkService = Engine.GetService<NetworkService>();
            _sceneSwitchingService = Engine.GetService<SceneSwitchingService>();
        }

        private void OnDestroy()
        {
            _clientBtn.onClick.RemoveListener(OnClickClientBtn);
            _hostBtn.onClick.RemoveListener(OnClickHostBtn);
        }

        private void OnClickClientBtn()
        {
            _sceneSwitchingService.LoadScene(_mapsDropdown.GetDropDownEnumValue<EScene>());
            _networkService.NetworkManager.StartClient();
            gameObject.SetActive(false);
        }

        private void OnClickHostBtn()
        {
            _sceneSwitchingService.LoadScene(_mapsDropdown.GetDropDownEnumValue<EScene>());
            _networkService.NetworkManager.StartHost();
            gameObject.SetActive(false);
        }
    }
}