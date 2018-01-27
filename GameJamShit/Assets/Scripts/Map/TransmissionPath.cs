using UnityEngine.Networking;

namespace Assets.Scripts.Map
{
    public class TransmissionPath : NetworkBehaviour
    {
        public TransmissionNode FromNode;
        public TransmissionNode DestinationNode;
        // Use this for initialization
        public void Start ()
        {
            gameObject.name = "TransmissionPath";
        }
	
        // Update is called once per frame
        void Update () {
		
        }
    }
}
