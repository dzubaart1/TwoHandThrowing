using TwoHandThrowing.Core;
using UnityEngine;

namespace TwoHandThrowing.Gameplay
{
    public class BallSpawner : MonoBehaviour
    {
        private BallConfigurationService _ballConfigurationService;

        private Ball _ballPrefab;
        private Transform _ballHolder;

        private void Awake()
        {
            _ballConfigurationService = Engine.GetService<BallConfigurationService>();
        }

        public void SetBallPrefab(Ball prefab)
        {
            _ballPrefab = prefab;
        }

        public GameObject Spawn(Vector3 spawnPoint, Vector3 force)
        {
            var ball = Instantiate(_ballPrefab, _ballHolder);
            ball.SetBallConfiguration(_ballConfigurationService.BallConfiguration);

            ball.transform.position = spawnPoint;
            ball.AddForce(force);

            return ball.gameObject;
        }
    }
}
