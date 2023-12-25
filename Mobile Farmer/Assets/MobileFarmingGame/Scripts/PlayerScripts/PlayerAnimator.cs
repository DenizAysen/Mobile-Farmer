using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem waterParticles;

    [Header("Settings")]
    [SerializeField] private float moveSpeedMultiplier;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void ManageAnimations(Vector3 moveVector)
    {
        if(moveVector.magnitude > 0)
        {
            animator.SetFloat("moveSpeed", moveVector.magnitude * moveSpeedMultiplier);
            PlayRunAnimation();

           animator.transform.forward = moveVector.normalized;
        }
        else
        {
            PlayIdleAnimation();
        }
    }
    public void PlayRunAnimation()
    {
        animator.Play("Run");
    }
    public void PlayIdleAnimation()
    {
        animator.Play("Idle");
    }
    #region SowAnim
    public void PlaySowAnimation()
    {
        animator.SetLayerWeight(1, 1);
    }
    public void StopSowAnimation()
    {
        animator.SetLayerWeight(1, 0);
    }
    #endregion
    #region WaterAnim
    public void StopWaterAnimation()
    {
        animator.SetLayerWeight(2, 0);
        waterParticles.Stop();
    }
    public void PlayWaterAnimation()
    {
        animator.SetLayerWeight(2, 1);
    }
    #endregion
    #region HarvestAnim
    public void PlayHarvestAnimation()
    {
        animator.SetLayerWeight(3, 1);
    }
    public void StopHarvestAnimation()
    {
        animator.SetLayerWeight(3, 0);
    }
    #endregion
    public void PlayShakeTreeAnimation()
    {
        animator.SetLayerWeight(4, 1);
        animator.Play("Shake Tree");
    }
    public void StopShakeAnimation()
    {
        animator.SetLayerWeight(4, 0);
    }
}
