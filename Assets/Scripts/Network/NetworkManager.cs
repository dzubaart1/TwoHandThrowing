using Mirror;
using System;
using TwoHandThrowing.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TwoHandThrowing.Network
{
    public class NetworkManager : Mirror.NetworkManager
    {
        public event Action ServerAddPlayerEvent;
        public event Action ServerStartedEvent;
        public event Action ServerConnectedEvent;
        public event Action StartClientEvent;

        public override void Start()
        {
            base.Start();

            Engine.GetService<NetworkService>().SetNetworkManager(this);
        }

        public override void OnStartClient()
        {
            base.OnStartServer();

            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            StartClientEvent?.Invoke();
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            ServerStartedEvent?.Invoke(); 
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            var netPlayer = Instantiate(playerPrefab).GetComponent<NetworkPlayer>();
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