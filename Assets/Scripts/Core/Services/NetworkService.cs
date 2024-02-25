using TwoHandThrowing.Network;

namespace TwoHandThrowing.Core
{
    public class NetworkService : IService
    {
        public NetworkPlayer CurrentNetworkPlayer;
        public NetworkManager NetworkManager;

        public void Initialize()
        {
        }

        public void Destroy()
        {
        }

        public void SetNetworkManager(NetworkManager networkManager)
        {
            NetworkManager = networkManager;
        }

        public void StartHost()
        {
            NetworkManager.StartHost();
        }

        public void StartClient()
        {
            NetworkManager.StartClient();
        }

    }
}
