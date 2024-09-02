using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls playerControls;
    private InputAction movementInput;
    [SerializeField] private float speed = 10.0f;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        movementInput = playerControls.Player.Movement;
        movementInput.Enable();
    }

    private void OnDisable()
    {
        movementInput.Disable();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementInput.ReadValue<Vector2>().x, movementInput.ReadValue<Vector2>().y, 0);
        movement.Normalize();
        movement *= speed * Time.fixedDeltaTime;
        transform.position += movement;
    }
}
