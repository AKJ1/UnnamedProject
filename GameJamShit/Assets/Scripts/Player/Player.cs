using System;
using System.Linq;
using Assets.Scripts.Map;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Player
{
    public class Player : NetworkBehaviour, IEquatable<Player>
    {
        public float SpawnInterval;

        public GameObject NewsPrefab;

        public PlayerEntity Entity;
        public TransmissionNode MainTransmissionNode;

        public GameObject NewsSpawnPoin;
        [SyncVar]
        public Color Color;

        public void Start()
        {
            Color = isLocalPlayer ? Color.blue : Color.red;
            var hq = transform.GetChild(0);
            hq.GetComponent<SpriteRenderer>().color = Color;

            if (isServer)
            {
                InvokeRepeating("SpawnCycle",3.0f,SpawnInterval);
            }
        }

        [UsedImplicitly]
        private void SpawnCycle()
        {
            CmdSpawnNews();
        }

        [Command]
        private void CmdSpawnNews()
        {
            SpawnNews();
        }

        [ClientRpc]
        private void SpawnNews()
        {
            var newsObject = Instantiate(NewsPrefab);
            newsObject.transform.position = NewsSpawnPoin.transform.position;

            MainTransmissionNode.NewsEntities.Enqueue(newsObject.GetComponent<NewsEntity>());
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

        public bool Equals(Player other)
        {
            return !ReferenceEquals(null, other) && ReferenceEquals(this, other);
        }
    }

    public enum PlayerEntity
    {
        Blue, Red
    }
}