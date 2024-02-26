using TwoHandThrowing.BallStuff;
using UnityEngine;

namespace TwoHandThrowing.Core
{
    public class BallConfigurationService : IService
    {
        public BallUIConfiguration Configuration { get; private set; }

        public BallConfiguration BallConfiguration { get; private set; }
        public GameObject TEMPObject;

        private NetworkService _networkService;
        public BallConfigurationService(BallUIConfiguration configuration, NetworkService networkService)
        {
            Configuration = configuration;
            _networkService = networkService;
        }

        public void Initialize()
        {
            BallConfiguration = new BallConfiguration();

            TEMPObject = new GameObject("TEMP");
            TEMPObject.SetActive(false);


            _networkService.StartClientEvent += OnStartClient;
        }

        public void Destroy()
        {
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

        private void OnStartClient()
        {
            Object.Instantiate(Configuration.UI);
        }
    }
}
