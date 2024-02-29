using TwoHandThrowing.Core;

namespace Assets.Scripts.Core.Services
{
    public class HandDataService : IService
    {
        public HandDataConfiguration Configuration { get; private set; }
        
        public HandDataService(HandDataConfiguration configuration)
        {
            Configuration = configuration;
        }

        public HandDataSettings GetSettingsByType(HandDataType handDataType)
        {
            return Configuration.HandDataSettings.Find(setting => setting.HandDataType.Equals(handDataType));
        }

        public void Initialize()
        {
        }
        
        public void Destroy()
        {
        }
    }
}
