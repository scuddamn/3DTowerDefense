using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int coordinates;
    public bool isTraversable;
    public bool isExplored;
    public bool isPath;
    public Node connectedTo;

    public Node(Vector2Int coordinates, bool isTraversable)
    {
        this.coordinates = coordinates;
        this.isTraversable = isTraversable;
    }
}
