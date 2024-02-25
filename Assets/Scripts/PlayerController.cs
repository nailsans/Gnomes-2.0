using CodeMonkey;
using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private InputAction move;
    private InputAction attack;
    private PlayerAttack _playerAttack;


    private Rigidbody rb;
    [SerializeField] private float movementForce = 1.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float maxSpeed = 5.0f;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField] private Camera playerCamera;

    private void Awake()
    {
        _playerAttack = GetComponent<PlayerAttack>();
        rb = GetComponent<Rigidbody>();
        playerInputActions = new PlayerInputActions();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void OnEnable()
    {
        playerInputActions.Player.Jump.started += DoJump;
        playerInputActions.Player.Attack.started += doAttack;

        move = playerInputActions.Player.Move;
        playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Player.Jump.started -= DoJump;
        playerInputActions.Player.Attack.started -= doAttack;

        playerInputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        doMove();

        RotateTowardsMouse();

    }

    private void doMove()
    {
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;


        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0f;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void DoJump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            forceDirection += Vector3.up * jumpForce;
        }
    }

    private void doAttack(InputAction.CallbackContext context)
    {
        //implement a hit in a direction that model of player is already looking
        _playerAttack.Attack();
    }

    private bool IsGrounded()
    {
        Vector3 rayStart = transform.position + Vector3.up * 0.1f;
        float rayLength = 1f;
        if (Physics.Raycast(rayStart, Vector3.down, rayLength))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void RotateTowardsMouse()
    {
        Vector3 direction = getMousePos();
            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, float.MaxValue);
            }
    }

    private Vector3 getMousePos()
    {
        Vector3 direction = Vector3.zero;
        Vector3 objectPosition = transform.position;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            direction = point - objectPosition;
            direction.y = 0;
        }

        return direction;
    }
}
