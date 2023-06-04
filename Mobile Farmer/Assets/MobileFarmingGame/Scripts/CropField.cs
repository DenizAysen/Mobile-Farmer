using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CropField : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform TilesParent;
    private List<CropTile> cropTiles = new List<CropTile>();

    [Header("Settings")]
    [SerializeField] private CropData cropData;
    private int _tilesSown = 0;
    private int _tilesWatered = 0;
    private int _tilesHarvested = 0;
    private TileFieldState _state;
    void Start()
    {
        _state = TileFieldState.Empty;
        StoreTiles();
    }

    [Header("Actions")]
    public static Action<CropField> onFullySown;
    public static Action<CropField> onFullyWatered;
    public static Action<CropField> onFullyHarvested;
    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.Space))
    //        InstantlySownTiles();
    //}
    private void StoreTiles()
    {
        for (int i = 0; i < TilesParent.childCount; i++)
        {
            cropTiles.Add(TilesParent.GetChild(i).GetComponent<CropTile>());
        }
    }
    public void SeedsCollidedCallBack(Vector3[] seedPositons)
    {
        for (int i = 0; i < seedPositons.Length; i++)
        {
            CropTile _closestCropTile = GetClosestCropTile(seedPositons[i]);

            if (_closestCropTile == null)
                continue;

            if (!_closestCropTile.IsEmpty())
                continue;

            Sow(_closestCropTile);
        }     
    }
    public void WaterCollidedCallBack(Vector3[] waterPositons)
    {
        for (int i = 0; i < waterPositons.Length; i++)
        {
            CropTile _closestCropTile = GetClosestCropTile(waterPositons[i]);

            if (_closestCropTile == null)
                continue;

            if (!_closestCropTile.IsSown())
                continue;

            Water(_closestCropTile);
        }
    }
    private CropTile GetClosestCropTile(Vector3 seedPosition)
    {
        float minDistance = 1000;
        int closestCropTileIndex = -1;
        for (int i = 0; i < cropTiles.Count; i++)
        {
            CropTile cropTile = cropTiles[i];
            float distanceTileSeed = Vector3.Distance(cropTile.transform.position,seedPosition);
            if (distanceTileSeed < minDistance)
            {
                minDistance = distanceTileSeed;
                closestCropTileIndex = i;
            }
        }
        if(closestCropTileIndex == -1)
        {
            return null;
        }
        return cropTiles[closestCropTileIndex];
    }
    #region Sow
    private void Sow(CropTile cropTile)
    {
        cropTile.Sow(cropData);
        _tilesSown++;

        if (_tilesSown == cropTiles.Count)
            FieldFullySown();
    }
    private void FieldFullySown()
    {
        Debug.Log("Field fully sown");
        _state = TileFieldState.Sown;
        onFullySown?.Invoke(this);
    }
    #endregion
    #region Water
    private void Water(CropTile cropTile)
    {
        cropTile.Water();
        _tilesWatered++;
        if (_tilesWatered == cropTiles.Count)
            FieldFullyWatered();
    }
    private void FieldFullyWatered()
    {
        Debug.Log("Field fully watered");
        _state = TileFieldState.Watered;
        onFullyWatered?.Invoke(this);
    }
    #endregion
    #region Harvest
    public void Harvest(Transform harvestSphere)
    {
        float _sphereRadius = harvestSphere.localScale.x;

        for (int i = 0; i < cropTiles.Count; i++)
        {
            if (cropTiles[i].IsEmpty())
                continue;

            float distanceCropSphere = Vector3.Distance(harvestSphere.position, cropTiles[i].transform.position);

            if (distanceCropSphere < _sphereRadius)
                HarvestTile(cropTiles[i]);
        }
    }
    private void HarvestTile(CropTile cropTile)
    {
        cropTile.Harvest();

        _tilesHarvested++;
        if (_tilesHarvested == cropTiles.Count)
            FieldFullyHarvested();
    }
    private void FieldFullyHarvested()
    {
        _tilesSown = 0;
        _tilesWatered = 0;
        _tilesHarvested = 0;

        _state = TileFieldState.Empty;

        onFullyHarvested?.Invoke(this);
    }
    #endregion
    #region NaughtyAttributes
    [NaughtyAttributes.Button]
    private void InstantlySownTiles()
    {
        foreach (CropTile cropTile in cropTiles)
        {
            Sow(cropTile);
        }
    }
    [NaughtyAttributes.Button]
    private void InstantlyWaterTiles()
    {
        foreach (CropTile cropTile in cropTiles)
        {
            Water(cropTile);
        }
    }
    #endregion
    #region States
    public bool IsEmpty()
    {
        return _state == TileFieldState.Empty;
    }
    public bool IsSown()
    {
        return _state == TileFieldState.Sown;
    }
    public bool IsWatered()
    {
        return _state == TileFieldState.Watered;
    }
    #endregion
}
