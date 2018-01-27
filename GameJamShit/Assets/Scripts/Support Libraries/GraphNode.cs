using System.Collections.Generic;

public class GraphNode<T>
{
    public GraphNode(T value)
    {
        Value = value;
        IsTraversed = false;
    }
    public bool IsTraversed = false;

    public List<GraphNode<T>> Neighbours;

    public T Value;
}
