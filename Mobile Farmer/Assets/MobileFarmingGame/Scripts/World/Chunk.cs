using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Chunk : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject unlockedElements;
    [SerializeField] private GameObject lockedElements;
    [SerializeField] private TextMeshPro priceText;

    [Header(" Settings ")]
    [SerializeField] private int initialPrice;
    private int _currentPrice;
    private bool unlocked = false;

    [Header("Actions")]
    public static Action onUnlocked;
    public static Action onPriceChanged;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Initialaze(int Loadedprice)
    {
        _currentPrice = Loadedprice;
        priceText.text = _currentPrice.ToString();

        if (_currentPrice <= 0)
            Unlock(false);
    }

    #region Unlock Methods
    public void TryUnlock()
    {
        if (CashManager.instance.GetCoins() <= 0)
            return;

        _currentPrice--;
        CashManager.instance.UseCoins(1);
        onPriceChanged?.Invoke();

        priceText.text = _currentPrice.ToString();

        if (_currentPrice <= 0)
            Unlock();
    }
    private void Unlock(bool triggerAction = true)
    {
        unlockedElements.SetActive(true);
        lockedElements.SetActive(false);
        unlocked = true;

        if(triggerAction)
            onUnlocked?.Invoke();
    }
    public bool IsUnlocked()
    {
        return unlocked;
    }
#endregion
    #region GetPriceMethods
    public int GetCurrentPrice()
    {
        return _currentPrice;
    }
    public int GetInitialPrice()
    {
        return initialPrice;
    }
    #endregion
}
