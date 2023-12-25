using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AppleTreeManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Slider shakeSlider;

    [Header("Settings")]
    private AppleTree _lastTriggeredTree;

    [Header("Actions")]
    public static Action<AppleTree> onTreeModeStarted;
    public static Action onTreeModeEnded;
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
        if (!_lastTriggeredTree.IsAppleTreeReady())
        {
            return;
        }

        StartTreeMode();
    }
    #endregion
    private void StartTreeMode()
    {
        _lastTriggeredTree.Initialize(this);

        onTreeModeStarted?.Invoke(_lastTriggeredTree);

        UpdateShakeSlider(0);
    }
    public void EndTreeMode()
    {
        onTreeModeEnded?.Invoke();
    }
    public void UpdateShakeSlider(float value)
    {
        shakeSlider.value = value;
    }
}
