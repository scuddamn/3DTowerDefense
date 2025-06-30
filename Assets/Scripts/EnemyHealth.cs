using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHP = 5;

    [SerializeField] private int currentHP = 0;

    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        currentHP = maxHP;
    }
    
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        currentHP--;
        if (currentHP <= 0)
        {
            gameObject.SetActive(false);
            enemy.RewardGold();
        }
    }
}
