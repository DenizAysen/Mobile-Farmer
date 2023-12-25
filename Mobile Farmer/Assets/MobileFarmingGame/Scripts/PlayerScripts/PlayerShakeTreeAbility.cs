using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
public class PlayerShakeTreeAbility : MonoBehaviour
{
    [Header("Components")]
    private PlayerAnimator _playerAnimator;
    [Header("Settings")]
    [SerializeField] private float distanceToTree;
    [Range(0f,1f)]
    [SerializeField] private float shakeThreshold = .05f;
    private bool _isActive;
    private bool _isShaking;
    private Vector2 previousMousePos;

    [Header("Elements")]
    private AppleTree _currentTree;

    private void Awake()
    {
        SubscribeEvents();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }
    private void SubscribeEvents()
    {
        AppleTreeManager.onTreeModeStarted += TreeModeStartedCallBack;
        AppleTreeManager.onTreeModeEnded += TreeModeEndedCallBack;
    }

    private void OnDestroy()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
        AppleTreeManager.onTreeModeStarted -= TreeModeStartedCallBack;
        AppleTreeManager.onTreeModeEnded -= TreeModeEndedCallBack;
    }
    private void Update()
    {
        if (_isActive && !_isShaking)
        {
            ManageTreeShaking();
        }
    }
    #region CallBacks
    private void TreeModeStartedCallBack(AppleTree appleTree)
    {
        _currentTree = appleTree;

        _isActive = true;

        MoveTowardsTree();
    }
    private void TreeModeEndedCallBack()
    {
        _currentTree = null;

        _isActive = false;

        _isShaking = false;

        LeanTween.delayedCall(.1f, () => _playerAnimator.StopShakeAnimation());
    } 
    #endregion
    private void MoveTowardsTree()
    {
        Vector3 treePos = _currentTree.transform.position;
        Vector3 dir = transform.position - treePos;

        Vector3 flatDir = dir;
        flatDir.y = 0f;

        Vector3 targetPos = treePos + flatDir.normalized * distanceToTree;
        _playerAnimator.ManageAnimations(-flatDir);

        LeanTween.move(gameObject, targetPos, .5f);
    }
    private void ManageTreeShaking()
    {
        if (!Input.GetMouseButton(0))
        {
            _currentTree.StopShaking();
            return;
        }

        float shakeMagnitude = Vector2.Distance(Input.mousePosition, previousMousePos);

        if (ShouldShake(shakeMagnitude))
            Shake();
        else
            _currentTree.StopShaking();

        previousMousePos = Input.mousePosition;
    }

    private bool ShouldShake(float shakeMagnitude)
    {
        float screenPercent = shakeMagnitude / Screen.width;

        return screenPercent >= shakeThreshold;
    }

    private void Shake()
    {
        _isShaking = true;

        _currentTree.Shake();

        _playerAnimator.PlayShakeTreeAnimation();

        LeanTween.delayedCall(.1f, () => _isShaking = false);
    }
}
