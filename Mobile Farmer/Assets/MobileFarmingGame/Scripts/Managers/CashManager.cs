using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CashManager : MonoBehaviour
{
    [Header("Settings")]
    private int _coins = 0;
    public static CashManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        LoadData();
        UpdateCoinContainers();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void UpdateCoinContainers()
    {
        GameObject[] coinContainers = GameObject.FindGameObjectsWithTag("CoinAmount");

        foreach (GameObject coinContainer in coinContainers)
        {
            coinContainer.GetComponent<TextMeshProUGUI>().text = _coins.ToString();
        }
    }
    public void AddCoins(int amount)
    {
        _coins += amount;
        UpdateCoinContainers();
        Debug.Log("We have " + _coins + " coins");

        SaveData();
    }
    public void UseCoins(int amount)
    {
        AddCoins(-amount);
    }
    public int GetCoins()
    {
        return _coins;
    }
    [NaughtyAttributes.Button]
    private void Add500Coins()
    {
        AddCoins(500);
    }
    private void LoadData()
    {
        _coins = PlayerPrefs.GetInt("Coins");
    }
    private void SaveData()
    {
        PlayerPrefs.SetInt("Coins", _coins);
    }
}
