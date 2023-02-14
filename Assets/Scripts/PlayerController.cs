using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private Vector2 input;
    private CharacterController _characterController;
    private Vector3 direction;

    [SerializeField] private float smoothTime = 0.5f;
    private float currentVelocity;

    [SerializeField] private float speed;

    private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float velocity;

    [SerializeField] private float jumpPower;

    public void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }
    public void Update()
    {
        ApplyGravity();
        ApplyRotation();
        ApplyMovement();
    }

    private void ApplyRotation()
    {
        if (input.sqrMagnitude == 0) return;
        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void ApplyMovement()
    {
        _characterController.Move(direction * speed * Time.deltaTime);
    }

    public void ApplyGravity()
    {
        if (IsGrounded() && velocity < 0.0f)
        {
            velocity = -1.0f;
        }
        else
        {
            velocity += gravity * gravityMultiplier * Time.deltaTime;
            direction.y = velocity;
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0.0f, input.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {

        if (!context.started) return;
        if (!IsGrounded()) return;
        velocity = jumpPower;
    }

    private bool IsGrounded() => _characterController.isGrounded;
}
