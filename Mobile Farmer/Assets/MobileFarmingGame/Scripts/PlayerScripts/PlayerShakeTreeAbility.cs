using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
public class PlayerShakeTreeAbility : MonoBehaviour
{
    [Header("Componenets")]
    private PlayerAnimator _playerAnimator;

    [Header("Settings")]
    [SerializeField] private float distanceToTree;

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
    }
    private void TreeModeStartedCallBack(AppleTree appleTree)
    {
        _currentTree = appleTree;

        MoveTowardsTree();
    }

    private void OnDestroy()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
        AppleTreeManager.onTreeModeStarted -= TreeModeStartedCallBack;
    }

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
}
