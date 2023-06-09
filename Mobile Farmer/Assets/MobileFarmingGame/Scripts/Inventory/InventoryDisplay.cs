using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform cropContainersParent;
    [SerializeField] private UICropContainer uiCropContainerPrefab;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Configure(Inventory inventory)
    {
        InventoryItem[] items = inventory.GetInventoryItems();

        for (int i = 0; i < items.Length; i++)
        {
            UICropContainer uICropContainer = Instantiate(uiCropContainerPrefab, cropContainersParent);
            //
            Sprite sprite = DataManager.instance.GetCropSpriteFromCropType(items[i].cropType);

            uICropContainer.Configure(sprite, items[i].amount);
        }
    }
    public void UpdateDisplay(Inventory inventory)
    {
        InventoryItem[] items = inventory.GetInventoryItems();

        UICropContainer containerInstance;

        for (int i = 0; i < items.Length; i++)
        {
            if(i < cropContainersParent.childCount)
            {
                containerInstance = cropContainersParent.GetChild(i).GetComponent<UICropContainer>();
                containerInstance.gameObject.SetActive(true);
            }
            else
            {
                containerInstance = Instantiate(uiCropContainerPrefab, cropContainersParent);
            }

            Sprite sprite = DataManager.instance.GetCropSpriteFromCropType(items[i].cropType);
            containerInstance.Configure(sprite, items[i].amount);
        }

        int remainingContainers = cropContainersParent.childCount - items.Length;

        if (remainingContainers <= 0)
            return;

        for (int i = 0; i < remainingContainers; i++)
        {
            cropContainersParent.GetChild(items.Length + i).gameObject.SetActive(false);
        }

        /*
        //First Way

        //while(cropContainersParent.childCount > 0)
        //{
        //    Transform container = cropContainersParent.GetChild(0);
        //    container.SetParent(null);
        //    Destroy(container.gameObject);
        //}

        //Configure(inventory);

        //for (int i = 0; i < items.Length; i++)
        //{
        //    UICropContainer uICropContainer = Instantiate(uiCropContainerPrefab, cropContainersParent);
        //    //
        //    Sprite sprite = DataManager.instance.GetCropSpriteFromCropType(items[i].cropType);

        //    uICropContainer.Configure(sprite, items[i].amount);
        //}
        */
    }
}
