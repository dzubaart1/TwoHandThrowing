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
        public VRNetworkManager NetworkManager { get; private set; }
        
        [CanBeNull]
        public NetworkPlayer CurrentNetworkPlayer { get; private set; }
        
        [CanBeNull]
        public VRNetworkBehaviour NetworkBehaviour { get; private set; }
        
        public void Initialize()
        {
            // Nothing to initialize
        }

        public void SetNetworkManager(VRNetworkManager manager)
        {
            NetworkManager = manager;
            CurrentNetworkManagerEvent?.Invoke();
        }
        
        public void SetCurrentNetworkPlayer(NetworkPlayer player)
        {
            CurrentNetworkPlayer = player;
            CurrentNetworkPlayerSetEvent?.Invoke();
        }

        public void SetNetworkBehaviour(VRNetworkBehaviour networkBehaviour)
        {
            NetworkBehaviour = networkBehaviour;
            NetworkBehaviourSpawnedEvent?.Invoke();
        }
    }
}
