using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
public class PlayerSowAbility : MonoBehaviour
{
    [Header("Elements")]
    private PlayerAnimator _playerAnimator;
    void Start()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    //void Update()
    //{
        
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            _playerAnimator.PlaySowAnimation();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            _playerAnimator.StopSowAnimation();
        }
    }
}
