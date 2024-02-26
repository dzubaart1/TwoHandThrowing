using Mirror;
using TwoHandThrowing.Network;

namespace TwoHandThrowing.Core
{
    public class NetworkService : IService
    {
        public Network.NetworkPlayer CurrentNetworkPlayer { get; private set; }
        public Network.NetworkManager NetworkManager { get; private set; }
        public Network.NetworkBehaviour NetworkBehaviour { get; private set; }

        public void Initialize()
        {
        }

        public void Destroy()
        {
        }

        public void SetCurrentNetworkPlayer(Network.NetworkPlayer networkPlayer)
        {
            CurrentNetworkPlayer = networkPlayer;
        }

        public void SetNetworkManager(Network.NetworkManager networkManager)
        {
            NetworkManager = networkManager;
        }

        public void SetNetworkBehaviour(Network.NetworkBehaviour behaviour)
        {
            NetworkBehaviour = behaviour;
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
