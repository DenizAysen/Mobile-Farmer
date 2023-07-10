using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Chunk : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject unlockedElements;
    [SerializeField] private GameObject lockedElements;
    [SerializeField] private TextMeshPro priceText;

    [Header(" Settings ")]
    [SerializeField] private int initialPrice;
    private int _currentPrice;
    void Start()
    {
        priceText.text = initialPrice.ToString();
        _currentPrice = initialPrice;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TryUnlock()
    {
        if (CashManager.instance.GetCoins() <= 0)
            return;

        _currentPrice--;
        CashManager.instance.UseCoins(1);
        priceText.text = _currentPrice.ToString();

        if (_currentPrice <= 0)
            Unlock();
    }
    private void Unlock()
    {
        unlockedElements.SetActive(true);
        lockedElements.SetActive(false);

    }
}
