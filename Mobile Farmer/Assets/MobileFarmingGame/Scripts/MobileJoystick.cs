using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileJoystick : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] RectTransform joystickOutline;
    Vector3 _clickedPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClickedOnJoystickZoneCallback()
    {
        _clickedPosition = Input.mousePosition;
        joystickOutline.position = _clickedPosition;
    }
}
