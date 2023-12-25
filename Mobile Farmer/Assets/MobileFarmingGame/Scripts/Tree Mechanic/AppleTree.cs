using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject treeCam;
    [SerializeField] private Renderer renderer;
    [SerializeField] private Transform appleParent;
    private AppleTreeManager treeManager;

    [Header("Settings")]
    [SerializeField] private float maxShakeMagnitude;
    [SerializeField] private float shakeIncrement;
    private float _shakeSliderValue;
    private float shakeMagnitude;
    private bool _isShaking;

    [Header("Actions")]
    public static Action<CropType> onAppleCollected;
    public void Initialize(AppleTreeManager treeManager)
    {
        this.treeManager = treeManager;
        _shakeSliderValue = 0;
        EnableCam();
    }
    public void EnableCam()
    {
        treeCam.SetActive(true);
    }
    public void DisableCam()
    {
        treeCam.SetActive(false);
    }
    public void Shake()
    {
        _isShaking = true;

        TweenShake(maxShakeMagnitude);

        UpdateShakeSlider();
    }
    public void StopShaking()
    {
        if (!_isShaking)
            return;

        Debug.Log("Stop Shaking");

        _isShaking = false;

        TweenShake(0);
    }
    public bool IsAppleTreeReady()
    {
        for (int i = 0; i < appleParent.childCount; i++)
        {
            Apple apple = appleParent.GetChild(i).GetComponent<Apple>();
            if (!apple.IsAppleReady())
            {
                return false;
            }
        }
        return true;
    }
    private void TweenShake(float targetMagnitude)
    {
        LeanTween.cancel(renderer.gameObject);
        LeanTween.value(renderer.gameObject, UpdateShakeMagnitude, shakeMagnitude, targetMagnitude, 1f);
    }

    private void UpdateShakeMagnitude(float value)
    {
        shakeMagnitude = value;
        UpdateMaterials();
    }
    private void UpdateShakeSlider()
    {
        _shakeSliderValue += shakeIncrement;
        treeManager.UpdateShakeSlider(_shakeSliderValue);

        for (int i = 0; i < appleParent.childCount; i++)
        {
            float applePercent = (float) i / appleParent.childCount;

            Apple currentApple = appleParent.GetChild(i).GetComponent<Apple>();

            if (_shakeSliderValue > applePercent && !currentApple.IsFree())
            {
                ReleaseApple(currentApple);
            }
        }
        if (_shakeSliderValue >= 1)
            ExitTreeMode();
    }
    private void ReleaseApple(Apple apple)
    {
        apple.Release();

        onAppleCollected?.Invoke(CropType.Apple);
    }

    private void UpdateMaterials()
    {
        foreach (Material mat in renderer.materials)
        {
            mat.SetFloat("_Magnitude", shakeMagnitude);
        }
        foreach (Transform appleTransform in appleParent)
        {
            Apple apple = appleTransform.GetComponent<Apple>();

            if (apple.IsFree())
                continue;

            apple.Shake(shakeMagnitude);
        }
    }
    private void ExitTreeMode()
    {
        treeManager.EndTreeMode();

        DisableCam();

        TweenShake(0);

        ResetApples();
    }

    private void ResetApples()
    {
        for (int i = 0; i < appleParent.childCount; i++)
        {
            appleParent.GetChild(i).GetComponent<Apple>().Reset();
        }
    }
}
