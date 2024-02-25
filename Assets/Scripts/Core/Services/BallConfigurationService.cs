using System;
using System.Threading.Tasks;
using TwoHandThrowing.BallStuff;
using TwoHandThrowing.UI;
using UnityEngine;

namespace TwoHandThrowing.Core
{
    public class BallConfigurationService : IService
    {
        public event Action UpdateBallConfigurationEvent;

        public BallConfiguration BallConfiguration { get; private set; }
        public GameObject TEMPObject;

        public void Initialize()
        {
            BallConfiguration = new BallConfiguration();

            TEMPObject = new GameObject("TEMP");
            TEMPObject.SetActive(false);
        }

        public void Destroy()
        {
        }

        public void UpdateBallConfiguration(Rigidbody rigidbody, PhysicMaterial physicMaterial)
        {
            BallConfiguration.RigidBody = rigidbody;
            BallConfiguration.PhysicMaterial = physicMaterial;

            UpdateBallConfigurationEvent?.Invoke();
        }

    }
}
