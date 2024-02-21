using MaterialFactory.BallStuff;
using MaterialFactory.UI;
using Services;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Services
{
    public class BallConfigurationService : IService
    {
        public event Action UpdateBallConfigurationEvent;

        public BallConfiguration BallConfiguration { get; private set; }
        public BallConfigurationUI BallConfigurationUI { get; private set; }
        public GameObject TEMPObject;

        private const string UI_PATH = "Prefabs/BallConfigurationUI";

        private BallConfigurationUI _ballConfigurationUIPrefab;

        public Task Initialize()
        {
            BallConfiguration = new BallConfiguration();

            TEMPObject = new GameObject("TEMP");
            TEMPObject.SetActive(false);

            _ballConfigurationUIPrefab = Resources.Load<BallConfigurationUI>(UI_PATH);
            BallConfigurationUI = UnityEngine.Object.Instantiate(_ballConfigurationUIPrefab);

            return Task.CompletedTask;
        }

        public void UpdateBallConfiguration(Rigidbody rigidbody, PhysicMaterial physicMaterial)
        {
            BallConfiguration.RigidBody = rigidbody;
            BallConfiguration.PhysicMaterial = physicMaterial;

            UpdateBallConfigurationEvent?.Invoke();
        }
    }
}
