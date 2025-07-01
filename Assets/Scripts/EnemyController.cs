using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<Node> path = new List<Node>();

    [Header("Movement Speed")]
    [SerializeField] [Range(1f, 5f)] private float speed = 1f;

    private Enemy enemy;
    private GridManager gridManager;
    private Pathfinder pathfinder;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindFirstObjectByType<GridManager>();
        pathfinder = FindFirstObjectByType<Pathfinder>();
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
        for(int i = 0; i < path.Count; i++)
        {
            //print(waypoint.name);
            Vector3 startPos = transform.position;
            Vector3 nextPos = gridManager.GetPositionFromCoords(path[i].coordinates);

            float travelPercent = 0f;
            
            transform.LookAt(nextPos);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, nextPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    } 

    void FindPath()
    {
        path.Clear();
        path = pathfinder.GetNewPath();
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoords(pathfinder.StartCoordinates);
    }

    void FinishPath()
    {
            enemy.PenalizeGold();
            gameObject.SetActive(false);
        
        
    }
    
}
