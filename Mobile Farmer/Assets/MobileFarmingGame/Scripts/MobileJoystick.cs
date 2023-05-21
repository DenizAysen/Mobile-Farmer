using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileJoystick : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] RectTransform joystickOutline;
    [SerializeField] RectTransform joystickKnob;

    [Header("Settings")]
    [SerializeField] private float _moveFactor;
    private bool canControl;
    private Vector3 _move;
    Vector3 _clickedPosition;
    void Start()
    {
        HideJoystick();
    }

    // Update is called once per frame
    void Update()
    {
        if (canControl)
        {
            ControlJoystick();
        }
    }
    public void ClickedOnJoystickZoneCallback()
    {
        _clickedPosition = Input.mousePosition;
        joystickOutline.position = _clickedPosition;

        ShowJoystick();
    }
    private void ShowJoystick()
    {
        joystickOutline.gameObject.SetActive(true);
        canControl = true;
    }
    private void HideJoystick()
    {
        joystickOutline.gameObject.SetActive(false);
        canControl = false;

        _move = Vector3.zero;
    }
    private void ControlJoystick()
    {
        Vector3 _currentPosition = Input.mousePosition;
        Vector3 _direction = _currentPosition - _clickedPosition;

        float _moveMagnitude = _direction.magnitude * _moveFactor / Screen.width;

        _moveMagnitude = Mathf.Min(_moveMagnitude, joystickOutline.rect.width / 2);

        _move = _direction.normalized * _moveMagnitude;

        Vector3 _targetPosition = _clickedPosition + _move;

        joystickKnob.position = _targetPosition;

        if (Input.GetMouseButtonUp(0))
        {
            HideJoystick();
        }
    }
    public Vector3 GetMoveVector()
    {
        return _move;
    }
}
