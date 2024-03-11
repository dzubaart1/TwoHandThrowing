using Mirror;
using System;
using TwoHandThrowing.Core;
using UnityEngine.SceneManagement;

namespace TwoHandThrowing.Network
{
    public class VRNetworkManager : Mirror.NetworkManager
    {
        public event Action ServerAddPlayerEvent;
        public event Action ServerStartedEvent;
        public event Action ServerConnectedEvent;
        public event Action StartClientEvent;

        private NetworkService _networkService;

        public override void Awake()
        {
            base.Awake();

            _networkService = Engine.GetService<NetworkService>();
        }

        public override void Start()
        {
            base.Start();

            _networkService.SetNetworkManager(this);
        }

        public override void OnStartClient()
        {
            base.OnStartServer();
            
            StartClientEvent?.Invoke();
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            ServerStartedEvent?.Invoke(); 
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            NetworkPlayer netPlayer = Instantiate(playerPrefab).GetComponent<NetworkPlayer>();
            NetworkServer.AddPlayerForConnection(conn, netPlayer.gameObject);
            
            ServerAddPlayerEvent?.Invoke();
        }

        public override void OnServerConnect(NetworkConnectionToClient conn)
        {
            base.OnServerConnect(conn);

            ServerConnectedEvent?.Invoke();
        }
    }
}