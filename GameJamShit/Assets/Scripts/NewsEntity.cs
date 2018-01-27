using Assets.Scripts.Map;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public class NewsEntity : NetworkBehaviour
    {

        public Player.Player Owner;
        public TransmissionNode Destination;

        [ClientRpc]
        public void RpcSendTo(GameObject to)
        {
            var toNode = to.GetComponent<TransmissionNode>();

            transform.position = toNode.transform.position;
            toNode.NewsEntities.Enqueue(this);
        }

        // Use this for initialization
        void Start () {
	    	
        }
	
        // Update is called once per frame
        void Update () {
		
        }
    }
}
