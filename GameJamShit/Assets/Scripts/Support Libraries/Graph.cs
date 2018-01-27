using System.Collections.Generic;

public class Graph<T>
{
    public Graph()
    {
        ActiveNodes = new List<GraphNode<T>>();    
    }

    public List<GraphNode<T>> ActiveNodes;

    public LinkedList<GraphNode<T>> TraverseNodes(GraphNode<T> startNode, GraphNode<T> endNode, Queue<GraphNode<T>> queue = null)
    {
        startNode.IsTraversed = true;

        if (startNode == endNode)
        {
            var ll = new LinkedList<GraphNode<T>>();
            ll.AddFirst(startNode);
            return ll;
        }

        foreach (var node in startNode.Neighbours)
        {
            if (queue == null)
            {
                queue = new Queue<GraphNode<T>>();
            }
            if (!node.IsTraversed)
            {
                queue.Enqueue(node);
            }
        }
        while (queue.Count > 0)
        {
            var result = TraverseNodes(queue.Dequeue(), endNode, queue);
            if (result != null)
            {
                result.AddFirst(startNode);
            }
            return result;
        }
        return null;
    }
}
