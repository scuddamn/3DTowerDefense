using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private Vector2Int startCoords;
    [SerializeField] private Vector2Int endCoords;

    public Vector2Int StartCoordinates => startCoords;
    public Vector2Int EndCoordinates => endCoords;
    
    private Node startNode;
    private Node endNode;
    private Node currentSearchNode; //node being searched at current moment

    private Queue<Node> frontier = new();
    
    private Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

    private GridManager gridManager;

    private Dictionary<Vector2Int, Node> grid = new();
    private Dictionary<Vector2Int, Node> reached = new();

    private void Awake()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        
        if (gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = grid[startCoords];
            endNode = grid[endCoords];
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetNewPath();
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;

            if (grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
                
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isTraversable)
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void BreadthFirstSearch()
    {
        startNode.isTraversable = true;
        endNode.isTraversable = true;
        
        frontier.Clear();
        reached.Clear();
        
        bool isRunning = true;
        
        frontier.Enqueue(startNode);
        
        reached.Add(startCoords, startNode);

        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();

            if (currentSearchNode.coordinates == endCoords)
            {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }
        
        path.Reverse();
        return path;
    }

    public List<Node> GetNewPath()
    {
        gridManager.ResetNodes();
        BreadthFirstSearch();
        return BuildPath();
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool prevState = grid[coordinates].isTraversable;
            grid[coordinates].isTraversable = false;
            List<Node> newPath = GetNewPath();

            grid[coordinates].isTraversable = prevState;

            if (newPath.Count <= 1) //if there is only one block in the path
            {
                GetNewPath(); //try again to find a new path
                return true;
            }
        }

        return false;
    }
    
}
