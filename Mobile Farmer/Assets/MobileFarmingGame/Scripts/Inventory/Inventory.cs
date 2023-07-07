using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    [SerializeField] private List<InventoryItem> items = new List<InventoryItem>();
    private InventoryItem _item;
    public void CropHarvestedCallBack(CropType cropType)
    {
        bool _cropFound = false;

        for (int i = 0; i < items.Count; i++)
        {
            _item = items[i];
            
            if(_item.cropType == cropType)
            {
                _item.amount++;
                _cropFound = true;
                break;
            }
        }

        //DebugInventory();

        if (_cropFound)
            return;

        items.Add(new InventoryItem(cropType, 1));

    }
    public InventoryItem[] GetInventoryItems()
    {
        return items.ToArray();
    }
    public void DebugInventory()
    {
        foreach (var inventoryItem in items)
        {
            Debug.Log("We have " + inventoryItem.amount + " items in our " + inventoryItem.cropType + " list");
        }
    }
    public void Clear()
    {
        items.Clear();
    }
}
