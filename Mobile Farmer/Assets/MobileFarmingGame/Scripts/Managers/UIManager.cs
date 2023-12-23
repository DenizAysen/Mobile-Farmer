using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject treeModePanel;

    [SerializeField] private GameObject treeButton;
    [SerializeField] private GameObject toolButtonsContainer;
    private void Awake()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PlayerChunkDetection.onEnteredAppleZone += EnteredTreeZoneCallBack;
        PlayerChunkDetection.onExitedAppleZone += ExitedTreeZoneCallBack;

        AppleTreeManager.onTreeModeStarted += SetTreeMode;
    }

    private void OnDestroy()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
        PlayerChunkDetection.onEnteredAppleZone -= EnteredTreeZoneCallBack;
        PlayerChunkDetection.onExitedAppleZone -= ExitedTreeZoneCallBack;

        AppleTreeManager.onTreeModeStarted += SetTreeMode;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetGameMode();
    }

    private void EnteredTreeZoneCallBack(AppleTree tree)
    {
        treeButton.SetActive(true);
        toolButtonsContainer.SetActive(false);
    }
    private void ExitedTreeZoneCallBack(AppleTree tree)
    {
        treeButton.SetActive(false);
        toolButtonsContainer.SetActive(true);
    }
    private void SetGameMode()
    {
        gamePanel.SetActive(true);
        treeModePanel.SetActive(false);
    }
    private void SetTreeMode()
    {
        treeModePanel.SetActive(true);
        gamePanel.SetActive(false);
    }
}
