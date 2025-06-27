using System;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private bool isPlaceable;
    [SerializeField] private List<GameObject> turrets;
    [SerializeField] private GameObject turretPrefab;
    public void OnMouseDown()
    {
        if (isPlaceable)
        {
            print($"Waypoint clicked: {transform.name}");
            Instantiate(turretPrefab, transform.position, Quaternion.identity);
            isPlaceable = false;
        }
    }
}
