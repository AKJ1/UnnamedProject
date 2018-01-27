using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionPath : MonoBehaviour
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
