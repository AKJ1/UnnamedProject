using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Newtworking
{
    public class CommunicationLayer : NetworkBehaviour
    {

        public GameObject Map;

        [Command]
        public void CmdPathClicked(GameObject path)
        {
            
        }

        // Use this for initialization
        public void Start () {
		        
        }
	
        // Update is called once per frame
        public void Update ()
        {
            if (!isLocalPlayer) { return; }

            if (true/*HitRaycast*/)
            {
                
            }
        }
    }
}
