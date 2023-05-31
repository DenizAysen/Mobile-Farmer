using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] private Transform cropRenderer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ScaleUp()
    {
        cropRenderer.gameObject.LeanScale(Vector3.one, 1f).setEase(LeanTweenType.easeOutBack);
    }
}
