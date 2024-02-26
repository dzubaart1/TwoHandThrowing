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
        public void CmdSpawn(NetworkIdentity owned, Vector3 spawnPoint, Vector3 force)
        {
            Debug.Log("here workds!");

            Engine.GetService<BallSpawnerService>().Spawn(owned, spawnPoint, force);
        }
    }
}
