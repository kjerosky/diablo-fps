using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour {

    public float walkSpeed = 2;
    public float sprintSpeed = 8;

    private const float GRAVITY = -9.8f;

    private GameInputs gameInputs;
    private Vector2 currentMovement;
    private bool sprintIsPressed;

    private CharacterController characterController;
    private Vector3 verticalVelocity;

    void Awake() {
        gameInputs = new GameInputs();
        gameInputs.Player.Move.started += handleMove;
        gameInputs.Player.Move.performed += handleMove;
        gameInputs.Player.Move.canceled += handleMove;
        gameInputs.Player.Sprint.started += handleSprint;
        gameInputs.Player.Sprint.performed += handleSprint;
        gameInputs.Player.Sprint.canceled += handleSprint;

        characterController = GetComponent<CharacterController>();
    }

    void OnEnable() {
        gameInputs.Player.Enable();
    }

    void OnDisable() {
        gameInputs.Player.Disable();
    }

    private void handleMove(InputAction.CallbackContext context) {
        currentMovement = context.ReadValue<Vector2>();
    }

    private void handleSprint(InputAction.CallbackContext context) {
        sprintIsPressed = context.ReadValueAsButton();
    }

    private void handleMovement() {
        float movementSpeed = walkSpeed;
        if (sprintIsPressed) {
            movementSpeed = sprintSpeed;
        }

        float strafeMove = currentMovement.x;
        float forwardMove = currentMovement.y;

        strafeMove *= movementSpeed * Time.deltaTime;
        forwardMove *= movementSpeed * Time.deltaTime;

        Vector3 movement = transform.right * strafeMove + transform.forward * forwardMove;
        characterController.Move(movement);
    }

    private void handleGravity() {
        verticalVelocity.y += GRAVITY * Time.deltaTime;
        characterController.Move(verticalVelocity * Time.deltaTime);

        if (characterController.isGrounded) {
            verticalVelocity.y = -0.1f;
        }
    }

    void Update() {
        handleMovement();
        handleGravity();
    }
}
