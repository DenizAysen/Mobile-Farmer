using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
public class PlayerSowAbility : MonoBehaviour
{
    [Header("Elements")]
    private PlayerAnimator _playerAnimator;

    [Header("Settings")]
    private CropField _currentCropField;
    void Start()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();

        SeedParticles.onSeedsCollided += SeedsCollidedCallBack;
    }
    private void OnDestroy()
    {
        SeedParticles.onSeedsCollided -= SeedsCollidedCallBack;
    }
    //void Update()
    //{

    //}
    private void SeedsCollidedCallBack(Vector3[] seedPositons)
    {
        if (_currentCropField == null)
        {
            return;
        }
        _currentCropField.SeedsCollidedCallBack(seedPositons);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            _playerAnimator.PlaySowAnimation();
            _currentCropField = other.GetComponent<CropField>();          
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
