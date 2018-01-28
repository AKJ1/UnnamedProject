using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Map
{
    public class PointGenerator : NetworkBehaviour
    {
        public int NumPts;

        public float MinDistance;

        public float MaxNodeLinks;

        public float MinNodeLinks;

        public GameObject PlayerBasePrefab;

        public GameObject LinkPrefab;

        public GameObject TransmissionNodePrefab;

        public List<TransmissionNode> SelectedNodes = new List<TransmissionNode>();

        public TransmissionNode StartNodeA;

        public TransmissionNode StartNodeB;

        public void Generate()
        {
            if (!isServer) return;
            ClientScene.RegisterPrefab(TransmissionNodePrefab);
            SelectNodes(NumPts);
            LinkNodes();
            var farthest = Get2Farthest(global::Map.Instance.ActiveNodes);
            Debug.Log(farthest[0]);
            StartNodeA = farthest[0];
            StartNodeB = farthest[1];
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Generate();
            }
        }

        public void SelectNodes(int numPointsFinal)
        {
            NumPts = numPointsFinal;

            while (SelectedNodes.Count < NumPts)
            {
                var xSelect = Random.Range(global::Map.Instance.Bounds.xMin, global::Map.Instance.Bounds.xMax);
                var ySelect = Random.Range(global::Map.Instance.Bounds.yMin, global::Map.Instance.Bounds.yMax);

                var selectPos = new Vector3(xSelect, 0, ySelect);
                var candidateNodes = global::Map.Instance.UnusedNodes.OrderBy(n => Vector3.Distance(n.transform.position, selectPos));
                var filteredCandidateNodes = candidateNodes
                    .Where(n => !global::Map.Instance.ActiveNodes
                        .Any(n2 => Vector3.Distance(n.transform.position, n2.transform.position) < MinDistance))
                    .ToList();

                if (filteredCandidateNodes.Any())
                {
                    var chosen = filteredCandidateNodes.First();
                    CmdCreateNode(chosen.gameObject);
                    global::Map.Instance.RegisterNode(chosen);
                    SelectedNodes.Add(chosen);
                }
            }
        }
        
        private void CmdCreateNode(GameObject chosen)
        {
            var antenna = Instantiate(TransmissionNodePrefab);
            antenna.transform.parent = chosen.transform;
            antenna.transform.localPosition = Vector3.zero;

            NetworkServer.Spawn(antenna);
        }

        public void LinkNodes()
        {
            foreach (var transmissionNode in SelectedNodes)
            {

                var numLinks = Mathf.RoundToInt(Random.Range(MinNodeLinks, MaxNodeLinks));
                transmissionNode.AllowedLinks = numLinks;
                var ordered = SelectedNodes.OrderBy(n => Vector3.Distance(transmissionNode.transform.position, n.transform.position)).Take(numLinks + 1).ToArray();

                var filtered = ordered.Where(
                    n => n != transmissionNode
                         && !transmissionNode.IsLinkedTo(n)).ToArray();
                for (var i = 0; i < Mathf.Min(numLinks, filtered.Count()); i++)
                {
                    Debug.Log("Linking nodes " + transmissionNode.transform.name + " and " + filtered[i].transform.name);
                    //RpcCreateRoads(transmissionNode, filtered[i]);
                    global::Map.Instance.LinkNodes(transmissionNode, filtered[i]);
                }
            }
        }
        
        private void RpcCreateRoads(TransmissionNode a, TransmissionNode b)
        {

            var road1 = Instantiate(LinkPrefab);
            var road2 = Instantiate(LinkPrefab);

            road1.transform.position = a.transform.position;
            road2.transform.position = b.transform.position;

            var distance = Vector3.Distance(a.transform.position, b.transform.position);
            var rend = road1.transform.GetChild(0).GetComponent<Renderer>();
            var nowSIze = rend.bounds.size.z;
            var targetsize = distance / 2;
            var sizeFactor = nowSIze / targetsize;
            road2.transform.localScale = new Vector3(road1.transform.localScale.x, road1.transform.localScale.y, road1.transform.localScale.z / sizeFactor);
            road1.transform.localScale = new Vector3(road1.transform.localScale.x, road1.transform.localScale.y, road1.transform.localScale.z / sizeFactor);

            road1.transform.LookAt(b.transform);
            road2.transform.LookAt(a.transform);

            road1.GetComponent<TransmissionPath>().FromNode = a;
            road1.GetComponent<TransmissionPath>().DestinationNode = b;

            road2.GetComponent<TransmissionPath>().FromNode = b;
            road2.GetComponent<TransmissionPath>().DestinationNode = a;
        }

        private TransmissionNode[] Get2Farthest(IReadOnlyCollection<TransmissionNode> nodes)
        {
            float distance = 0;
            TransmissionNode first = null, second = null;
            foreach (var transmissionNode in nodes)
            {
                var fathest = nodes.OrderByDescending(n => Vector3.Distance(transmissionNode.transform.position, n.transform.position))
                    .First();

                var currdst = Vector3.Distance(transmissionNode.transform.position, fathest.transform.position);
                if (currdst > distance)
                {
                    distance = currdst;
                    first = transmissionNode;
                    second = fathest;
                }
            }
            return new[] { first, second };

            throw new KodTakoaException("Does not work due to overscope xD");
        }
    }

    internal class KodTakoaException : Exception
    {
        public KodTakoaException(string doesNotWorkDueToOverscopeXd)
        {
            throw new NotImplementedException();
        }
    }
}