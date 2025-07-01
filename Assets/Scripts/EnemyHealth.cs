using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHP = 5;
    
    [Tooltip("adds this value to future enemy HP max when an enemy dies")] [SerializeField] int difficultyRamp = 1;

    private int currentHP = 0;
    
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
        if (currentHP <= 0 && !gameObject.CompareTag("Ally"))
        {
            gameObject.SetActive(false);
            maxHP += difficultyRamp;
            enemy.RewardGold();
        }
        else
        {
            gameObject.SetActive(false);
            enemy.PenalizeGold();
        }
    }
}
