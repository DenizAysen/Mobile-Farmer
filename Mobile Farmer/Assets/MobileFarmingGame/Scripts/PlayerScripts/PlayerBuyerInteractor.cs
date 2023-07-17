using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuyerInteractor : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private InventoryManager inventoryManager;
    Inventory _inventory;
    InventoryItem[] _items;
    int _coinsEarned = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Buyer"))
            SellCrops();
    }
    private void SellCrops()
    {
        _inventory = inventoryManager.GetInventory();
        _items = _inventory.GetInventoryItems();

        for (int i = 0; i < _items.Length; i++)
        {
            int itemPrice = DataManager.instance.GetCropPriceFromCropType(_items[i].cropType);
            _coinsEarned += itemPrice * _items[i].amount;
        }

        Debug.Log("We' ve earned " + _coinsEarned + " coins");

        //CashManager.instance.AddCoins(_coinsEarned);
        TransactionEffectManager.instance.PlayCoinParticles(_coinsEarned);

        inventoryManager.ClearInventory();

        _coinsEarned = 0;
    }
}
