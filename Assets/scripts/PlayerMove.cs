using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour {

    public float walkSpeed = 2;
    public float sprintSpeed = 8;
    public float dashSpeed = 100;
    public float dashSmoothingFactor = 10;

    private const float GRAVITY = -9.8f;

    private GameInputs gameInputs;
    private Vector2 currentMovement;
    private bool sprintIsPressed;

    private const float DASHING_MAGNITUDE_THRESHOLD = 0.2f;
    private bool dashWasPressed;
    private Vector3 currentDash;

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
        gameInputs.Player.Dash.started += handleDash;

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

    private void handleDash(InputAction.CallbackContext context) {
        dashWasPressed = context.ReadValueAsButton();
    }

    private void handleMovement() {
        bool shouldStartDash =
            dashWasPressed &&
            currentMovement.magnitude != 0 &&
            currentDash.magnitude < DASHING_MAGNITUDE_THRESHOLD;
        if (shouldStartDash) {
            currentDash = (transform.right * currentMovement.x + transform.forward * currentMovement.y) * dashSpeed;
        }

        Vector3 movement;
        if (currentDash.magnitude >= DASHING_MAGNITUDE_THRESHOLD) {
            movement = calculateDashMovement();
        } else {
            movement = calculateStandardMovement();
        }

        characterController.Move(movement);
    }

    private Vector3 calculateDashMovement() {
        // Idea from: https://answers.unity.com/questions/242648/force-on-character-controller-knockback.html
        currentDash = Vector3.Lerp(currentDash, Vector3.zero, dashSmoothingFactor * Time.deltaTime);
        return currentDash * Time.deltaTime;
    }

    private Vector3 calculateStandardMovement() {
        float strafeMove = currentMovement.x;
        float forwardMove = currentMovement.y;

        float movementSpeed = walkSpeed;
        if (sprintIsPressed) {
            movementSpeed = sprintSpeed;
        }

        strafeMove *= movementSpeed * Time.deltaTime;
        forwardMove *= movementSpeed * Time.deltaTime;

        return transform.right * strafeMove + transform.forward * forwardMove;
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

    void LateUpdate() {
        resetPressedButtonStatuses();
    }

    private void resetPressedButtonStatuses() {
        dashWasPressed = false;
    }
}
