using TwoHandThrowing.Core;
using UnityEngine;
using UnityEngine.UI;

namespace TwoHandThrowing.UI
{
    public class ConnectionUI : MonoBehaviour
    {
        [SerializeField] private Button _clientBtn;
        [SerializeField] private Button _hostBtn;

        private NetworkService _networkService;

        private void Awake()
        {
            _clientBtn.onClick.AddListener(OnClickClientBtn);
            _hostBtn.onClick.AddListener(OnClickHostBtn);

            _networkService = Engine.GetService<NetworkService>();
        }

        private void OnDestroy()
        {
            _clientBtn.onClick.RemoveListener(OnClickClientBtn);
            _hostBtn.onClick.RemoveListener(OnClickHostBtn);
        }

        private void OnClickClientBtn()
        {
            _networkService.StartClient();
            gameObject.SetActive(false);
        }

        private void OnClickHostBtn()
        {
            _networkService.StartHost();
            gameObject.SetActive(false);
        }
    }
}