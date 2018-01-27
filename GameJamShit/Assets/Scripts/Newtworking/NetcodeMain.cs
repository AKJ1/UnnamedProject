using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Newtworking
{
    public class NetcodeMain : MonoBehaviour
    {

        private NetworkManager _networkManager;

        // Use this for initialization
        public void Start ()
        {
            _networkManager = FindObjectOfType<NetworkManager>();
        }

        // Update is called once per frame
        public void Update () {
		
        }

        
        public void RemoteCall()
        {
            
        }
    }
}
