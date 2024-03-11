using Mirror;
using TwoHandThrowing.Core;

namespace TwoHandThrowing.Network
{
    public class VRNetworkBehaviour : NetworkBehaviour
    {
        public override void OnStartClient()
        {
            base.OnStartClient();

            Engine.GetService<NetworkService>().SetNetworkBehaviour(this);
        }

        [Command]
        public void CmdSpawnBall()
        {
            Engine.GetService<BallSpawnerService>().Spawn();
        }
    }
}
