using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private int turretCost = 75;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CreateTurret(Turret turret, Vector3 position)
    {
        Bank bank = FindFirstObjectByType<Bank>();
        if (bank == null) return false;


        if (bank.CurrentBalance >= turretCost)
        {
            Instantiate(turret, position, Quaternion.identity);
            bank.Withdraw(turretCost);
            return true;
        }

        return false;
    }
}
