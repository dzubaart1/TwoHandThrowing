using MaterialFactory.BallStuff;
using System;
using UnityEngine;

namespace MaterialFactory.Services
{
    public class BallConfigurationService
    {
        public event Action UpdateBallConfigurationEvent;

        public BallConfiguration BallConfiguration { get; private set; }
        public GameObject TEMPObject;

        private static BallConfigurationService _singleton;
        public static BallConfigurationService Instance()
        {
            return _singleton ??= new BallConfigurationService();
        }

        public BallConfigurationService()
        {
            BallConfiguration = new BallConfiguration();

            TEMPObject = new GameObject("TEMP");
            TEMPObject.SetActive(false);
        }

        public void UpdateBallConfiguration(Rigidbody rigidbody, PhysicMaterial physicMaterial)
        {
            BallConfiguration.RigidBody = rigidbody;
            BallConfiguration.PhysicMaterial = physicMaterial;

            UpdateBallConfigurationEvent?.Invoke();
        }
    }
}
