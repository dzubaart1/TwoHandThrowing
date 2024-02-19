using MaterialFactory.BallStuff;
using System;
using UnityEngine;

namespace MaterialFactory.Services
{
    public class BallSpawnerService
    {
        public event Action SpawnEvent;

        private static BallSpawnerService _singleton;
        public static BallSpawnerService Instance()
        {
            return _singleton ??= new BallSpawnerService();
        }

        private BallSpawner _ballSpawner;
        private Vector3 _spawnPoint;
        private Vector3 _force;
        
        public BallSpawnerService()
        {
            _ballSpawner = new GameObject("Spawner", typeof(BallSpawner)).GetComponent<BallSpawner>();
        }

        public void Spawn()
        {
            _ballSpawner.Spawn(_spawnPoint, _force);
            SpawnEvent?.Invoke();
        }

        public void SetSpawnConfig(Vector3 spawnPoint, Vector3 force)
        {
            _spawnPoint = spawnPoint;
            _force = force;
        }
    }
}
