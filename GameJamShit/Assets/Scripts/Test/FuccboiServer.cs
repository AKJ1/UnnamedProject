using UnityEngine.Networking;

namespace Assets.Scripts.Test
{
    public class FuccboiServer : NetworkManager
    {
        public Player.Player Player;

        public override void OnServerConnect(NetworkConnection conn)
        {
            base.OnServerConnect(conn);
            if (numPlayers != 0)
            {
                ;
                ClientScene.RegisterPrefab(FindObjectOfType<Assets.Scripts.Map.PointGenerator>().TransmissionNodePrefab);
            }
        }

        
    }
}
