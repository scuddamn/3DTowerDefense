using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Path")]
    [SerializeField] private List<Waypoint> path = new List<Waypoint>();

    [Header("Movement Speed")]
    [SerializeField] [Range(1f, 5f)] private float speed = 1f;

    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FollowPath() //move enemy along selected path of tiles
    {
        foreach (Waypoint waypoint in path)
        {
            //print(waypoint.name);
            Vector3 startPos = transform.position;
            Vector3 endPos = waypoint.transform.position;

            float travelPercent = 0f;
            
            transform.LookAt(endPos);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        } FinishPath();
    } 

    void FindPath()
    {
        path.Clear();
        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach(Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();
            if (waypoint != null)
            {
                path.Add(waypoint);
            }
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    void FinishPath()
    {
        enemy.PenalizeGold();
        gameObject.SetActive(false);
    }
    
}
