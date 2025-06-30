using System;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private bool isPlaceable;
    [SerializeField] private List<GameObject> turrets;
    [SerializeField] private Turret turretPrefab;

    public bool IsPlaceable => isPlaceable;

    public void OnMouseDown()
    {
        if (isPlaceable)
        {
            print($"Waypoint clicked: {transform.name}");
            bool isPlaced = turretPrefab.CreateTurret(turretPrefab, transform.position);
            isPlaceable = !isPlaced;
        }
    }
}
