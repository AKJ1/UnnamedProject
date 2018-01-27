using System.Collections.Generic;

public class Map : MonoSingleton<Map>
{
    public Graph<TransmissionNode> Graph;

    public void RegisterNode(TransmissionNode node)
    {
        Graph.ActiveNodes.Add(new GraphNode<TransmissionNode>(node));
    }

    public LinkedList<GraphNode<TransmissionNode>> PathFromTo(GraphNode<TransmissionNode> start,
        GraphNode<TransmissionNode> end)
    {
        return Graph.TraverseNodes(start, end);
    }
}