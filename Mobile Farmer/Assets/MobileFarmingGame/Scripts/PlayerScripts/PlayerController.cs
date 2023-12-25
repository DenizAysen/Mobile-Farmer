using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private MobileJoystick joystick;
    private CharacterController _characterController;
    private PlayerAnimator _playerAnimator;
    [Header("Settings")]
    [SerializeField]private float _moveSpeed;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageMovement();
    }
    private void ManageMovement()
    {
        Vector3 _moveVector = joystick.GetMoveVector() *_moveSpeed * Time.deltaTime / Screen.width;

        _moveVector.z = _moveVector.y;
        _moveVector.y = 0;

        _characterController.Move(_moveVector);

        if(transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        _playerAnimator.ManageAnimations(_moveVector);
    }
}
