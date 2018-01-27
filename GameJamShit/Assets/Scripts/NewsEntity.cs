using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.Networking;

public class NewsEntity : NetworkBehaviour
{

    public Player Owner;
    public TransmissionNode Destination;

    [ClientRpc]
    public void RpcSendTo(GameObject to)
    {
        var toNode = to.GetComponent<TransmissionNode>();
    }

	// Use this for initialization
	void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
