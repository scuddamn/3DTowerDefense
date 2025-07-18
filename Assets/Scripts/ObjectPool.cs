using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class ObjectPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    
    private GameObject enemyPrefab;

    [SerializeField][Range(0.1f, 20f)] private float spawnRate = 1f;

    [SerializeField][Range(0, 20)] private int poolSize = 5;

    private GameObject[] enemyPool;


    private void Awake()
    {
        PopulatePool();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjInPool();
            //Instantiate(enemyPrefab, transform);
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void PopulatePool()
    {
        enemyPool = new GameObject[poolSize];

        for (int i = 0; i < enemyPool.Length; i++)
        {
            RandomizePrefab();
            enemyPool[i] = Instantiate(enemyPrefab, transform);
            enemyPool[i].SetActive(false);
        }
    }

    void EnableObjInPool()
    {
        for (int i = 0; i < enemyPool.Length; i++)
        {
            if (enemyPool[i].activeInHierarchy == false)
            {
                enemyPool[i].SetActive(true);
                return;
            }
        }
    }

    void RandomizePrefab()
    {
        enemyPrefab = enemies[Random.Range(0, enemies.Count)];
    }
}
