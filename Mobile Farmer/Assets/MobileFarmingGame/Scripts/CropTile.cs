using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileFieldState { Empty, Sown, Watered}
public class CropTile : MonoBehaviour
{
    private TileFieldState state;
    [Header("Elements")]
    [SerializeField] private Transform cropParent;
    Crop _crop;
    void Start()
    {
        state = TileFieldState.Empty;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }  
    public bool IsEmpty()
    {
        return state == TileFieldState.Empty;
    }
    public void Sow(CropData cropData)
    {
        state = TileFieldState.Sown;
        
       _crop = Instantiate(cropData.cropPrefab,transform.position,Quaternion.identity,cropParent);
    }
}
