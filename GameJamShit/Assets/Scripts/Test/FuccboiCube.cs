using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Test
{
    public class FuccboiCube : NetworkBehaviour
    {
        public void Update()
        {
            if(!isLocalPlayer)return;
            
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
            var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

            transform.Rotate(0, x, 0);
            transform.Translate(0, 0, z);
        }
    }
}
