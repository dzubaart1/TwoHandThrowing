using System;
using JetBrains.Annotations;
using TwoHandThrowing.Network;

namespace TwoHandThrowing.Core
{
    public class NetworkService : IService
    {
        public event Action NetworkBehaviourSpawnedEvent;
        public event Action CurrentNetworkPlayerSetEvent;
        public event Action CurrentNetworkManagerEvent;
        
        [CanBeNull]
        public NetworkManager NetworkManager { get; private set; }
        
        [CanBeNull]
        public NetworkPlayer CurrentNetworkPlayer { get; private set; }
        
        [CanBeNull]
        public NetworkBehaviour NetworkBehaviour { get; private set; }
        
        public void Initialize()
        {
            // Nothing to initialize
        }

        public void SetNetworkManager(NetworkManager manager)
        {
            NetworkManager = manager;
            CurrentNetworkManagerEvent?.Invoke();
        }
        
        public void SetCurrentNetworkPlayer(NetworkPlayer player)
        {
            CurrentNetworkPlayer = player;
            CurrentNetworkPlayerSetEvent?.Invoke();
        }

        public void SetNetworkBehaviour(NetworkBehaviour networkBehaviour)
        {
            NetworkBehaviour = networkBehaviour;
            NetworkBehaviourSpawnedEvent?.Invoke();
        }
    }
}
