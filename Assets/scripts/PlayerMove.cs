using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float walkSpeed = 2;
    public float runSpeed = 6;

    private const float GRAVITY = -9.8f;

    private CharacterController characterController;
    private Vector3 verticalVelocity;

    void Start() {
        characterController = GetComponent<CharacterController>();
    }

    private void handleMovement() {
        float movementSpeed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) {
            movementSpeed = runSpeed;
        }

        float strafeMove = (Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0);
        float forwardMove = (Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0);

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