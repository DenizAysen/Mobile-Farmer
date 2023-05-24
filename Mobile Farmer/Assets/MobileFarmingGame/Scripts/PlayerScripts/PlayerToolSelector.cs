using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerToolSelector : MonoBehaviour
{
    public enum Tool { None,Sow,Water,Harvest}
    private Tool _activeTool;

    [Header("Elements")]
    [SerializeField] private Image[] toolImages;

    [Header("Settings")]
    [SerializeField] private Color selectedToolColor;

    [Header("Actions")]
    public Action<Tool> onToolSelected;
    void Start()
    {
        SelectTool(0);
    }
    public void SelectTool(int toolIndex)
    {
        _activeTool = (Tool)toolIndex;
        for (int i = 0; i < toolImages.Length; i++)        
            toolImages[i].color = i == toolIndex ? selectedToolColor : Color.white;

        onToolSelected?.Invoke(_activeTool);
    }
    public bool CanSow()
    {
        return _activeTool == Tool.Sow;
    }
    public bool CanWater()
    {
        return _activeTool == Tool.Water;
    }
    public bool CanHarvest()
    {
        return _activeTool == Tool.Harvest;
    }
}
