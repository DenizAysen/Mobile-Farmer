using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerToolSelector))]
public class PlayerSowAbility : MonoBehaviour
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

        CropField.onFullySown += CropFieldFullySownCallBack;
        SeedParticles.onSeedsCollided += SeedsCollidedCallBack;
        _playerToolSelector.onToolSelected += ToolSelectedCallBack;
    }
    private void OnDestroy()
    {
        SeedParticles.onSeedsCollided -= SeedsCollidedCallBack;
        CropField.onFullySown -= CropFieldFullySownCallBack;
        _playerToolSelector.onToolSelected -= ToolSelectedCallBack;
    }
    //void Update()
    //{

    //}
    private void ToolSelectedCallBack(PlayerToolSelector.Tool selectedTool)
    {
        if (!_playerToolSelector.CanSow())
            _playerAnimator.StopSowAnimation();
    }
    private void SeedsCollidedCallBack(Vector3[] seedPositons)
    {
        if (_currentCropField == null)
        {
            return;
        }
        _currentCropField.SeedsCollidedCallBack(seedPositons);
    }
    private void CropFieldFullySownCallBack(CropField cropField)
    {
       if(cropField == _currentCropField)
        {
            _playerAnimator.StopSowAnimation();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CropField") && other.GetComponent<CropField>().IsEmpty())
        {
            _currentCropField = other.GetComponent<CropField>();
            EnteredCropField(_currentCropField);
        }
    }
    private void EnteredCropField(CropField cropField)
    {
        if(_playerToolSelector.CanSow())
           _playerAnimator.PlaySowAnimation();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            EnteredCropField(other.GetComponent<CropField>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            _playerAnimator.StopSowAnimation();
            _currentCropField = null;
        }
    }
}
