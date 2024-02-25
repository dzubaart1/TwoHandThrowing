using System;
using TwoHandThrowing.BallStuff;
using UnityEngine;

namespace TwoHandThrowing.Core
{
    public class BallSpawnerService : IService
    {
        public event Action SpawnEvent;

        private BallSpawner _ballSpawner;
        private Vector3 _spawnPoint;
        private Vector3 _force;

        public void Initialize()
        {
            _ballSpawner = new GameObject("Spawner", typeof(BallSpawner)).GetComponent<BallSpawner>();
        }

        public void Destroy()
        {
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
