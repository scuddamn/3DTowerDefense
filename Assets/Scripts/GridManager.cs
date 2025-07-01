using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;
    private Dictionary<Vector2Int, Node> grid = new();

    public Dictionary<Vector2Int, Node> Grid => grid; // public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
                Debug.Log($"Coordinates: {grid[coordinates].coordinates}\n" +
                          $"Traversable Status: {grid[coordinates].isTraversable}");
            }
        }
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }

        return null;
    }
    
    public void BlockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isTraversable = false;
        }
    }
    
    public Vector2Int GetCoordsFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(position.z / UnityEditor.EditorSnapSettings.move.z);
        return coordinates;
    }

    public Vector3 GetPositionFromCoords(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * UnityEditor.EditorSnapSettings.move.x;
        position.z = coordinates.y * UnityEditor.EditorSnapSettings.move.z;
        return position;
    }

    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

   
}
