using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileFieldState { Empty, Sown, Watered}
public class CropTile : MonoBehaviour
{
    private TileFieldState state;
    [Header("Elements")]
    [SerializeField] private Transform cropParent;
    [SerializeField] private MeshRenderer tileRenderer;
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
    public bool IsSown()
    {
        return state == TileFieldState.Sown;
    }
    public void Sow(CropData cropData)
    {
        state = TileFieldState.Sown;
        
       _crop = Instantiate(cropData.cropPrefab,transform.position,Quaternion.identity,cropParent);
    }
    public void Water()
    {
        state = TileFieldState.Watered;

        _crop.ScaleUp();

        tileRenderer.gameObject.LeanColor(Color.white * .3f, 1f).setEase(LeanTweenType.easeOutBack);
    }
    public void Harvest()
    {
        state = TileFieldState.Empty;

        _crop.ScaleDown();

        tileRenderer.gameObject.LeanColor(Color.white , 1f);
    }
}
