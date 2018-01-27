using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Player
{
    public class Player : NetworkBehaviour, IEquatable<Player>
    {
        public PlayerEntity Entity;
        public GameObject SoldierSpawnPoint;
        [SyncVar]
        public Color Color;

        public void Start()
        {
            Color = isLocalPlayer ? Color.blue : Color.red;
            var hq = transform.GetChild(0);
            hq.GetComponent<SpriteRenderer>().color = Color;
            
        }

        public void Update()
        {
            if (!isLocalPlayer) return;

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.name == "TransmissionPath")
                    {
                        var path = hit.transform.GetComponent<TransmissionPath>();
                        if (path.FromNode.Owner == this && path.FromNode.NewsEntities.Any())
                        {
                            CmdAdvance(path.FromNode.gameObject, path.DestinationNode.gameObject);
                        }
                    }
                }
            }
        }

        [Command]
        public void CmdAdvance(GameObject from, GameObject to)
        {
            from.GetComponent<TransmissionNode>().NewsEntities.Dequeue().RpcSendTo(to); 
        }

        [Command]
        public void CmdTestRpc()
        {
            transform.Rotate(new Vector3(1,1),1.0f);
            Debug.Log("rpc called");
            
        }

        public bool Equals(Player other)
        {
            return !ReferenceEquals(null, other) && ReferenceEquals(this, other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Player) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ Color.GetHashCode();
            }
        }
    }

    public enum PlayerEntity
    {
        Blue, Red, Yellow, Green, Neutral
    }
}