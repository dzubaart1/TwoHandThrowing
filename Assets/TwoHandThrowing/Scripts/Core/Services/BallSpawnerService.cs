using Mirror;
using System;
using System.Collections.Generic;
using TwoHandThrowing.Gameplay;
using UnityEngine;

namespace TwoHandThrowing.Core
{
    public class BallSpawnerService : IService
    {
        public event Action SpawnEvent;

        public BallSpawnerConfiguration Configuration { get; private set; }

        private BallSpawner _ballSpawner;
        private List<Transform> _spawnPoints;

        public BallSpawnerService(BallSpawnerConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Initialize()
        {
            _ballSpawner = new GameObject("Spawner", typeof(BallSpawner)).GetComponent<BallSpawner>();
            _ballSpawner.SetBallPrefab(Configuration.BallPrefab);

            _spawnPoints = new List<Transform>();
        }

        public void GetSpawnPoint()
        {
            Transform spawnPoint = UnityEngine.Object.Instantiate(Configuration.SpawnPointPrefab, new Vector3(0,1,0), Quaternion.identity);
            _spawnPoints.Add(spawnPoint);
        }

        public void Spawn()
        {
            foreach(var spawnPoint in _spawnPoints)
            {
                GameObject obj = _ballSpawner.Spawn(spawnPoint.transform.position, Vector3.zero);
                NetworkServer.Spawn(obj);
            }

            SpawnEvent?.Invoke();
        }
    }
}
