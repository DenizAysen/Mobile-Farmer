using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Inventory _inventory;
    void Start()
    {
        _inventory = new Inventory();

        CropTile.onCropHarvested += CropHarvestedCallBack;
    }
    private void OnDestroy()
    {
        CropTile.onCropHarvested -= CropHarvestedCallBack;
    }

    private void CropHarvestedCallBack(CropType cropType)
    {
        _inventory.CropHarvestedCallBack(cropType);
    }
}
