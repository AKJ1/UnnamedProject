using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PointGenerator : MonoBehaviour
{
    public int NumPts;

    public float MinDistance;

    public float MaxNodeLinks;

    public float MinNodeLinks;

    public List<TransmissionNode> SelectedNodes = new List<TransmissionNode>();

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Generate(NumPts );
        }
    }

    public void Generate(int numPointsFinal)
    {
        SelectNodes(numPointsFinal);
        LinkNodes();
    }


    public void SelectMasterNodes()
    {
        
    }

    public void SelectNodes(int numPointsFinal)
    {
        NumPts = numPointsFinal;

        while (SelectedNodes.Count < NumPts)
        {
            float xSelect = Random.Range(Map.Instance.Bounds.xMin, Map.Instance.Bounds.xMax);
            float ySelect = Random.Range(Map.Instance.Bounds.yMin, Map.Instance.Bounds.yMax);

            Vector3 selectPos = new Vector3(xSelect, 0, ySelect);
            var candidateNodes = Map.Instance.UnusedNodes.OrderBy(n => Vector3.Distance(n.transform.position, selectPos));
            var filteredCandidateNodes = candidateNodes
                .Where(n => !Map.Instance.ActiveNodes
                            .Any(n2 => Vector3.Distance(n.transform.position, n2.transform.position) < MinDistance));
            Debug.Log(filteredCandidateNodes.Count());

            if (filteredCandidateNodes.Count() > 0)
            {
                var chosen = filteredCandidateNodes.First();
                Map.Instance.RegisterNode(chosen);
                SelectedNodes.Add(chosen);
            }
        }
    }

    public void LinkNodes()
    {
        foreach (var transmissionNode in SelectedNodes)
        {

            int numLinks = Mathf.RoundToInt(Random.Range(MinNodeLinks, MaxNodeLinks));
            transmissionNode.AllowedLinks = numLinks;
            var ordered = SelectedNodes.OrderBy(n => Vector3.Distance(transmissionNode.transform.position, n.transform.position)).Take(numLinks+1).ToArray();

            var filtered = ordered.Where(
                n => n != transmissionNode
                     && !transmissionNode.IsLinkedTo(n)).ToArray();
            for (int i = 0; i < Mathf.Min(numLinks, filtered.Count()); i++)
            {
                Debug.Log("Linking nodes " + transmissionNode.transform.name + " and " + filtered[i].transform.name);
                Map.Instance.LinkNodes(transmissionNode, filtered[i]);
            }
        }
    } 
}