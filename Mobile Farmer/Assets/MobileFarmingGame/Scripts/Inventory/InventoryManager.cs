using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(InventoryDisplay))]
public class InventoryManager : MonoBehaviour
{
    private Inventory _inventory;
    private InventoryDisplay _inventoryDisplay;
    private string _dataPath;
    void Start()
    {
        _dataPath = Application.dataPath + "/inventoryData.txt";
       
        LoadInventory();
        ConfigureInventoryDisplay();

        CropTile.onCropHarvested += CropHarvestedCallBack;
    }
    private void OnDestroy()
    {
        CropTile.onCropHarvested -= CropHarvestedCallBack;
    }
    private void ConfigureInventoryDisplay()
    {
        _inventoryDisplay = GetComponent<InventoryDisplay>();
        _inventoryDisplay.Configure(_inventory);
    }
    private void CropHarvestedCallBack(CropType cropType)
    {
        _inventory.CropHarvestedCallBack(cropType);

        _inventoryDisplay.UpdateDisplay(_inventory);

        SaveInventory();
    }

    [NaughtyAttributes.Button]
    private void ClearInventory()
    {
        _inventory.Clear();
        _inventoryDisplay.UpdateDisplay(_inventory);
        SaveInventory();
    }
    private void LoadInventory()
    {        
        string data = "";

        if (File.Exists(_dataPath))
        {
            data = File.ReadAllText(_dataPath);
            _inventory = JsonUtility.FromJson<Inventory>(data);

            if (_inventory == null)
                _inventory = new Inventory();
        }
        else
        {
            File.Create(_dataPath);
            _inventory = new Inventory();
        }        
    }
    private void SaveInventory()
    {
        string data = JsonUtility.ToJson(_inventory, true);
        File.WriteAllText(_dataPath, data);
    }
}
