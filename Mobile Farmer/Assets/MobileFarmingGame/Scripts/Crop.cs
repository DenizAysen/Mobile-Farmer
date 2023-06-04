using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] private Transform cropRenderer;
    [SerializeField] private ParticleSystem harvestedParticles;
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
    public void ScaleDown()
    {
        //cropRenderer.gameObject.LeanScale(Vector3.zero, 1f)
        //    .setEase(LeanTweenType.easeOutBack).setOnComplete(DestroyCrop);
        cropRenderer.gameObject.LeanScale(Vector3.zero, 1f)
            .setEase(LeanTweenType.easeOutBack).setOnComplete(()=>Destroy(gameObject));

        harvestedParticles.gameObject.SetActive(true);
        harvestedParticles.transform.parent = null;
        harvestedParticles.Play();
    }
    //private void DestroyCrop()
    //{
    //    Destroy(gameObject);
    //}
}
