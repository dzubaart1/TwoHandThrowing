using Mirror;
using System;
using TwoHandThrowing.BallStuff;
using UnityEngine;

namespace TwoHandThrowing.Core
{
    public class BallSpawnerService : IService
    {
        public event Action SpawnEvent;

        public BallSpawnerConfiguration Configuration { get; private set; }

        private BallSpawner _ballSpawner;

        public BallSpawnerService(BallSpawnerConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Initialize()
        {
            _ballSpawner = new GameObject("Spawner", typeof(BallSpawner)).GetComponent<BallSpawner>();
            _ballSpawner.SetBallPrefab(Configuration.BallPrefab);
        }

        public void Destroy()
        {
        }

        public void Spawn(NetworkIdentity owner, Vector3 spawnPoint, Vector3 force)
        {
            var obj = _ballSpawner.Spawn(spawnPoint, force);
            NetworkServer.Spawn(obj);
            obj.GetComponent<NetworkBehaviour>().netIdentity.AssignClientAuthority(owner.connectionToClient);
            SpawnEvent?.Invoke();
        }
    }
}
