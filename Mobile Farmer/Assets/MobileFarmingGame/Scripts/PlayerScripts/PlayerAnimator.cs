using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private Animator animator;

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
}
