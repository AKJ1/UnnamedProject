using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Map;
using UnityEngine;

[ExecuteInEditMode]
public class Map : MonoSingleton<Map>
{
    public Graph<TransmissionNode> Graph;

    public Rect Bounds; // y is z 

    public void Awake()
    {
        Graph = new Graph<TransmissionNode>();
    }

    public List<TransmissionNode> UnusedNodes;
    public List<TransmissionNode> AllNodes;
    public List<TransmissionNode> ActiveNodes;

    public void OnValidate()
    {
        var nodes = GameObject.FindObjectsOfType<TransmissionNode>();
        float minX, minY, maxX, maxY;
        AllNodes = new List<TransmissionNode>(nodes);
        UnusedNodes = new List<TransmissionNode>(nodes);
        minX = AllNodes.OrderBy(n => n.transform.position.x).Select(n => n.transform.position.x).First();
        minY = AllNodes.OrderBy(n => n.transform.position.z).Select(n => n.transform.position.z).First();
        maxX = AllNodes.OrderByDescending(n => n.transform.position.x).Select(n => n.transform.position.x).First();
        maxY = AllNodes.OrderByDescending(n => n.transform.position.z).Select(n => n.transform.position.z).First();
        ActiveNodes = new List<TransmissionNode>();
        Bounds = new Rect();
        Bounds.xMin = minX;
        Bounds.xMax = maxX;
        Bounds.yMax = maxY;
        Bounds.yMin = minY;
    }

    public void Update()
    {
        Debug.DrawLine(new Vector3(Bounds.xMin, 0, Bounds.yMin), new Vector3(Bounds.xMax, 0, Bounds.yMin), Color.red, 10);
        Debug.DrawLine(new Vector3(Bounds.xMax, 0, Bounds.yMin), new Vector3(Bounds.xMax, 0, Bounds.yMax), Color.red, 10);
        Debug.DrawLine(new Vector3(Bounds.xMax, 0, Bounds.yMax), new Vector3(Bounds.xMin, 0, Bounds.yMax), Color.red, 10);
        Debug.DrawLine(new Vector3(Bounds.xMin, 0, Bounds.yMax), new Vector3(Bounds.xMin, 0, Bounds.yMin), Color.red, 10);

    }
   


    public void RegisterNode(TransmissionNode node)
    {
        UnusedNodes.Remove(node);
        ActiveNodes.Add(node);
        Graph.ActiveNodes.Add(new GraphNode<TransmissionNode>(node));
    }

    public void LinkNodes(TransmissionNode first, TransmissionNode second)
    {
        first.Link(second);
        second.Link(first);
        Debug.DrawLine(first.transform.position, second.transform.position, Color.magenta, 10);
    }

    public LinkedList<GraphNode<TransmissionNode>> PathFromTo(GraphNode<TransmissionNode> start,
        GraphNode<TransmissionNode> end)
    {
        return Graph.TraverseNodes(start, end);
    }
}