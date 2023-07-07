using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private CropData[] cropDatas;

    public static DataManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public Sprite GetCropSpriteFromCropType(CropType cropType)
    {
        for (int i = 0; i < cropDatas.Length; i++)
        {
            if (cropDatas[i].cropType == cropType)
                return cropDatas[i].icon;
        }
        Debug.LogError("No icons found in cropData");
        return null;
    }
    public int GetCropPriceFromCropType(CropType cropType)
    {
        for (int i = 0; i < cropDatas.Length; i++)
        {
            if (cropDatas[i].cropType == cropType)
                return cropDatas[i].price;
        }
        Debug.LogError("No cropData found in that type");
        return 0;
    }
}
