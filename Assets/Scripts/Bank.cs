using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] private int startingBalance = 100;

    [SerializeField] private int currentBalance;
    
    [SerializeField] private TextMeshProUGUI bankText;

    public int CurrentBalance
    {
        get { return currentBalance; }
    }

    private GameManager gm; //removed

    private void Awake()
    {
        gm = FindFirstObjectByType<GameManager>(); //removed
        currentBalance = startingBalance;
        gm.UpdateBankText($"${currentBalance}"); //UpdateBankText();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        gm.UpdateBankText($"${currentBalance}");
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        gm.UpdateBankText($"${currentBalance}");

        if (currentBalance < 0)
        {
            Debug.Log("Game Over"); 
            GameOver();
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    // void UpdateBankText()
    // {
    //     bankText.text = $"${currentBalance}";
    // }
}
