using UnityEngine;

public class TargetLock : MonoBehaviour
{
    [SerializeField] private Transform weapon;
    [SerializeField] private ParticleSystem projectileParticles;
    [SerializeField] private float range = 15f;

    private Transform target;

    // Update is called once per frame
    void Update()
    {
        FindClosestEnemy();
        AimWeapon();
    }

    void AimWeapon()
    {
        if (target == null) return;
        
        float targetDistance = Vector3.Distance(transform.position, target.position);
        
        weapon.LookAt(target);

        if (targetDistance < range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void FindClosestEnemy()
    {
        EnemyController[] enemies = FindObjectsByType<EnemyController>(FindObjectsSortMode.None);

        Transform closestTarget = null;

        float maxDistance = Mathf.Infinity;

        foreach (EnemyController enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closestTarget;
    }

    void Attack(bool isActive)
    {
        var emissionModule = projectileParticles.emission;

        emissionModule.enabled = isActive;
    }
}
