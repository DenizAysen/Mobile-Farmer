using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTreeManager : MonoBehaviour
{
    [Header("Settings")]
    private AppleTree _lastTriggeredTree;

    [Header("Actions")]
    public static Action<AppleTree> onTreeModeStarted;
    private void Awake()
    {
        SubscribeEvents();
    }
    private void SubscribeEvents()
    {
        PlayerChunkDetection.onEnteredAppleZone += EnteredTreeZoneCallback;
    }
    private void OnDestroy()
    {
        UnSubscribeEvents();
    }
    private void UnSubscribeEvents()
    {
        PlayerChunkDetection.onEnteredAppleZone -= EnteredTreeZoneCallback;
    }
    // Start is called before the first frame update
    #region CallBacks
    private void EnteredTreeZoneCallback(AppleTree tree)
    {
        _lastTriggeredTree = tree;
    }
    public void TreeButtonCallBack()
    {
        Debug.Log("Tree Button Clicked");
        StartTreeMod();
    }

    private void StartTreeMod()
    {
        _lastTriggeredTree.EnableCam();

        onTreeModeStarted?.Invoke(_lastTriggeredTree);
    }
    #endregion
}
