using Mirror;
using TwoHandThrowing.BallStuff;
using TwoHandThrowing.Core;
using UnityEngine;

namespace TwoHandThrowing.Network
{
    public class NetworkBehaviour : Mirror.NetworkBehaviour
    {
        public override void OnStartClient()
        {
            base.OnStartClient();

            Engine.GetService<NetworkService>().SetNetworkBehaviour(this);
        }

        [Command]
        public void CmdSpawn(NetworkIdentity owned, Vector3 force)
        {
            Engine.GetService<BallSpawnerService>().Spawn(owned, force);
        }
    }
}
