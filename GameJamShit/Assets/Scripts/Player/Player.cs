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

        public NewsEntity NewsPrefab;
        
        public TransmissionNode MainTransmissionNode;

        public Color Color;


        [UsedImplicitly]
        private void SpawnCycle()
        {
            CmdSpawnNews();
        }

        [Command]
        private void CmdSpawnNews()
        {
            RpcSpawnNews();
        }

        [ClientRpc]
        private void RpcSpawnNews()
        {
            var newsObject = Instantiate(NewsPrefab);
            newsObject.transform.position = MainTransmissionNode.transform.position;

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

        public void Initialize()
        {
            Color = isLocalPlayer ? Color.blue : Color.red;
            var generator = FindObjectOfType<PointGenerator>();

            MainTransmissionNode = isServer ? generator.StartNodeA : generator.StartNodeB;

            var hq = MainTransmissionNode.transform.GetChild(0);
            hq.GetComponent<SpriteRenderer>().color = Color;

            if (isServer)
            {
                InvokeRepeating("SpawnCycle", 3.0f, SpawnInterval);
            }
        }
    }
}