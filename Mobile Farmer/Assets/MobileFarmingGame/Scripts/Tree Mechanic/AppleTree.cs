using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject treeCam;
    [SerializeField] private Renderer renderer;

    [Header("Settings")]
    [SerializeField] private float maxShakeMagnitude;
    private float shakeMagnitude;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        TweenShake(maxShakeMagnitude);
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

    private void UpdateMaterials()
    {
        foreach (Material mat in renderer.materials)
        {
            mat.SetFloat("_Magnitude", shakeMagnitude);
        }
    }
}
