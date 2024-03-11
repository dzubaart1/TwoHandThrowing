using TwoHandThrowing.Gameplay;
using UnityEngine;

namespace TwoHandThrowing.Core
{
    public class BallConfigurationService : IService
    {
        public BallConfiguration BallConfiguration { get; private set; }
        public GameObject TEMPObject;

        public void Initialize()
        {
            BallConfiguration = new BallConfiguration();

            TEMPObject = new GameObject("TEMP");
            TEMPObject.SetActive(false);
        }

        public void UpdateThrowOnDetach(float smoothingDurationSlider, float velocityScale, float angularVelocityScale)
        {
            BallConfiguration.SmoothingDuration = smoothingDurationSlider;
            BallConfiguration.VelocityScale = velocityScale;
            BallConfiguration.AngularVelocityScale = angularVelocityScale;
        }

        public void UpdateRigidBody(Rigidbody rigidbody)
        {
            BallConfiguration.RigidBody = rigidbody;
        }

        public void UpdatePhysicMaterial(PhysicMaterial physicMaterial)
        {
            BallConfiguration.PhysicMaterial = physicMaterial;
        }

        public void UpdateBallLifeTime(float lifeTime)
        {
            BallConfiguration.LifeTime = lifeTime;
        }
    }
}
