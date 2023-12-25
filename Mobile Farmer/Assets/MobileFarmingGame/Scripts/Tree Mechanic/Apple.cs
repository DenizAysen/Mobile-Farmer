using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Apple : MonoBehaviour
{
    enum AppleState
    {
        Ready,
        Growing
    }
    private AppleState _state;

    [Header("Elements")]
    [SerializeField] private Renderer renderer;
    private Rigidbody _rigidbody;

    [Header("Settings")]
    [SerializeField] private float shakeMultiplier;
    private Vector3 _initialPos;
    private Quaternion _initialRot;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _initialPos = transform.position;
        _initialRot = transform.rotation;
    }
    private void Start()
    {
        _state = AppleState.Ready;
    }
    public void Shake(float magnitude)
    {
        float realShakeMagnitude = magnitude * shakeMultiplier;

        renderer.material.SetFloat("_Magnitude", realShakeMagnitude);
    }
    public void Release()
    {
        _rigidbody.isKinematic = false;

        _state = AppleState.Growing;

        renderer.material.SetFloat("_Magnitude", 0);
    }
    public bool IsFree()
    {
        return !_rigidbody.isKinematic;
    }
    public void Reset()
    {
        LeanTween.scale(gameObject, Vector3.zero, 1f).setDelay(2f).setOnComplete(ForceReset);
    }
    public bool IsAppleReady()
    {
        return _state == AppleState.Ready;
    }
    private void ForceReset()
    {
        transform.position = _initialPos;
        transform.rotation = _initialRot;

        _rigidbody.isKinematic = true;
        // scale up
        float randomScaleTime = Random.Range(5f, 10f);
        LeanTween.scale(gameObject, Vector3.one, randomScaleTime).setOnComplete(SetReady);
    }

    private void SetReady()
    {
        _state = AppleState.Ready;
    }
}
