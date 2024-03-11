using JetBrains.Annotations;
using TwoHandThrowing.Core;
using TwoHandThrowing.Player;
using UnityEngine;

namespace Assets.Scripts.Core.Services
{
    public class HandDataConfigurationService : IService
    {
        public HandDataConfiguration Configuration { get; private set; }

        public HandDataConfigurationService(HandDataConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Initialize()
        {
            // Nothing to initialize
        }

        [CanBeNull]
        public HandDataSettings GetSettingsByType(HandDataType handDataType)
        {
            return Configuration.FindHandDataSettings(handDataType);
        }
        
        public void UpdateHandConfig(PhysicMaterial physicMaterial, float maxVelocityToAttach, HandCollision handCollision)
        {
            handCollision.UpdatePhysicMaterial(physicMaterial);
            handCollision.MaxVelocityToAttach = maxVelocityToAttach;
        }
    }
}
