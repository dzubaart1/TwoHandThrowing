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
        }
        
        public void Destroy()
        {
        }
        
        public HandDataSettings GetSettingsByType(HandDataType handDataType)
        {
            return Configuration.HandDataSettings.Find(setting => setting.HandDataType.Equals(handDataType));
        }
        
        public void UpdateHandConfig(PhysicMaterial physicMaterial, float minVelocityToAttach, HandRef handRef)
        {
            handRef.Collision.UpdatePhysicMaterial(physicMaterial);
            handRef.HandData.MinVelocityToAttach = minVelocityToAttach;
        }
    }
}
