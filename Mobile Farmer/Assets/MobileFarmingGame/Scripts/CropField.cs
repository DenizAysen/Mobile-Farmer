using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropField : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform TilesParent;
    private List<CropTile> cropTiles = new List<CropTile>();
    void Start()
    {
        StoreTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void StoreTiles()
    {
        for (int i = 0; i < TilesParent.childCount; i++)
        {
            cropTiles.Add(TilesParent.GetChild(i).GetComponent<CropTile>());
        }
        for (int i = 0; i < cropTiles.Count; i++)
        {
            Debug.Log(cropTiles[i].name);
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
    private void Sow(CropTile cropTile)
    {
        cropTile.Sow();
    }
}