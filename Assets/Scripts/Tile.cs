using System;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private bool isPlaceable;
    //[SerializeField] private List<GameObject> turrets; add later
    [SerializeField] private Turret turretPrefab;

    private GridManager gridManager;
    private Vector2Int coordinates; // = new();
    private Pathfinder pathfinder;
    
    public bool IsPlaceable => isPlaceable;

    private void Awake()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        pathfinder = FindFirstObjectByType<Pathfinder>();
    }

    private void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordsFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    public void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isTraversable && !pathfinder.WillBlockPath(coordinates))
        {
            print($"Tile clicked: {transform.name}");
            bool isPlaced = turretPrefab.CreateTurret(turretPrefab, transform.position);
            isPlaceable = !isPlaced;
            gridManager.BlockNode(coordinates);
        }
    }
}
