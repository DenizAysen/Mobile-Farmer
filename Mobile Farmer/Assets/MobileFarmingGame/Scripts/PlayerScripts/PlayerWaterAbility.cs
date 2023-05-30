using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerToolSelector))]
public class PlayerWaterAbility : MonoBehaviour
{
    [Header("Elements")]
    private PlayerAnimator _playerAnimator;
    private PlayerToolSelector _playerToolSelector;

    [Header("Settings")]
    private CropField _currentCropField;
    void Start()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerToolSelector = GetComponent<PlayerToolSelector>();

        CropField.onFullyWatered += CropFieldFullyWateredCallBack;

        WaterParticles.onWaterCollided += WaterCollidedCallBack;

        _playerToolSelector.onToolSelected += ToolSelectedCallBack;
    }
    private void OnDestroy()
    {
        WaterParticles.onWaterCollided -= WaterCollidedCallBack;

        CropField.onFullyWatered -= CropFieldFullyWateredCallBack;

        _playerToolSelector.onToolSelected -= ToolSelectedCallBack;
    }
    //void Update()
    //{

    //}
    private void ToolSelectedCallBack(PlayerToolSelector.Tool selectedTool)
    {
        if (!_playerToolSelector.CanWater())
            _playerAnimator.StopWaterAnimation();
    }
    private void WaterCollidedCallBack(Vector3[] waterPositons)
    {
        if (_currentCropField == null)
        {
            return;
        }
        _currentCropField.WaterCollidedCallBack(waterPositons);
    }
    private void CropFieldFullyWateredCallBack(CropField cropField)
    {
        if (cropField == _currentCropField)
        {
            _playerAnimator.StopWaterAnimation();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CropField") && other.GetComponent<CropField>().IsSown())
        {
            _currentCropField = other.GetComponent<CropField>();
            EnteredCropField(_currentCropField);
        }
    }
    private void EnteredCropField(CropField cropField)
    {
        if (_playerToolSelector.CanWater())
        {
            if (_currentCropField != cropField)
                _currentCropField = cropField;
            _playerAnimator.PlayWaterAnimation();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CropField") && other.GetComponent<CropField>().IsSown())
        {
            EnteredCropField(other.GetComponent<CropField>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            _playerAnimator.StopWaterAnimation();
            _currentCropField = null;
        }
    }
}
