using Mirror;
using System;
using TwoHandThrowing.Network;

namespace TwoHandThrowing.Core
{
    public class NetworkService : IService
    {
        public event Action ServerAddPlayerEvent;
        public event Action ServerStartedEvent;
        public event Action ServerConnectedEvent;
        public event Action NetworkBehaviourSpawnedEvent;
        public event Action StartClientEvent;

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

            NetworkManager.ServerAddPlayerEvent += OnServerAddPlayer;
            NetworkManager.ServerConnectedEvent += OnServerConnected;
            NetworkManager.ServerStartedEvent += OnServerStarted;
            NetworkManager.StartClientEvent += OnStartClient;
        }

        public void SetNetworkBehaviour(Network.NetworkBehaviour behaviour)
        {
            NetworkBehaviour = behaviour;
            NetworkBehaviourSpawnedEvent?.Invoke();
        }

        public void StartHost()
        {
            NetworkManager.StartHost();
        }

        public void StartClient()
        {
            NetworkManager.StartClient();
        }

        private void OnServerAddPlayer()
        {
            ServerAddPlayerEvent?.Invoke();
        }

        private void OnServerStarted()
        {
            ServerStartedEvent?.Invoke();
        }

        private void OnServerConnected()
        {
            ServerConnectedEvent?.Invoke();
        }

        private void OnStartClient()
        {
            StartClientEvent?.Invoke();
        }
    }
}
