using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerToolSelector))]
public class PlayerHarvestAbility : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform harvestSphere;
    private PlayerAnimator _playerAnimator;
    private PlayerToolSelector _playerToolSelector;

    [Header("Settings")]
    private CropField _currentCropField;
    private bool _canHarvest;
    void Start()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerToolSelector = GetComponent<PlayerToolSelector>();

        CropField.onFullyHarvested += CropFieldFullyHarvestedCallBack;

        //WaterParticles.onWaterCollided += WaterCollidedCallBack;

        _playerToolSelector.onToolSelected += ToolSelectedCallBack;
    }
    private void OnDestroy()
    {
        //WaterParticles.onWaterCollided -= WaterCollidedCallBack;

        CropField.onFullyHarvested -= CropFieldFullyHarvestedCallBack;

        _playerToolSelector.onToolSelected -= ToolSelectedCallBack;
    }
    //void Update()
    //{

    //}
    #region CallBacks
    private void ToolSelectedCallBack(PlayerToolSelector.Tool selectedTool)
    {
        if (!_playerToolSelector.CanHarvest())
            _playerAnimator.StopHarvestAnimation();
    }
    private void CropFieldFullyHarvestedCallBack(CropField cropField)
    {
        if (cropField == _currentCropField)
            _playerAnimator.StopHarvestAnimation();
    }
    public void HarvestingStartedCallBack()
    {
        _canHarvest = true;
    }
    public void HarvestingStoppedCallBack()
    {
        _canHarvest = false;
    }
    #endregion
    #region Collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CropField") && other.GetComponent<CropField>().IsWatered())
        {
            _currentCropField = other.GetComponent<CropField>();
            EnteredCropField(_currentCropField);
        }
    }
    private void EnteredCropField(CropField cropField)
    {
        if (_playerToolSelector.CanHarvest())
        {
            if (_currentCropField != cropField)
                _currentCropField = cropField;

            _playerAnimator.PlayHarvestAnimation();

            if (_canHarvest)
                _currentCropField.Harvest(harvestSphere);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CropField") && other.GetComponent<CropField>().IsWatered())
        {
            EnteredCropField(other.GetComponent<CropField>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            _playerAnimator.StopHarvestAnimation();
            _currentCropField = null;
        }
    }
    #endregion
}
