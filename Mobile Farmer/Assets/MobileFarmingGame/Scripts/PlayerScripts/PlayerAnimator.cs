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
    public void PlaySowAnimation()
    {
        animator.SetLayerWeight(1, 1);
    }
    public void StopSowAnimation()
    {
        animator.SetLayerWeight(1, 0);
    }
    public void StopWaterAnimation()
    {
        animator.SetLayerWeight(2, 0);
        waterParticles.Stop();
    }
    public void PlayWaterAnimation()
    {
        animator.SetLayerWeight(2, 1);
    }
}
