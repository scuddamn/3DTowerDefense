using System;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bankText;

    //private Bank bank;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //bank = FindFirstObjectByType<Bank>();
    }

    public void UpdateBankText(string text)
    {
        bankText.text = text; //bankText.text = $"${bank.CurrentBalance}"
    }
}
