using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph<T>
{
    public List<GraphNode<T>> ActiveNodes;

    public List<GraphNode<T>> TraverseNodes(GraphNode<T> startNode )
    {
        return new List<GraphNode<T>>();
    }
}
