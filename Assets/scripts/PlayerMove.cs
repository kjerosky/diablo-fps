using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float walkSpeed = 3;

    private CharacterController characterController;

    void Start() {
        characterController = GetComponent<CharacterController>();
    }

    void Update() {
        float strafeMove = (Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0);
        float forwardMove = (Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0);

        strafeMove *= walkSpeed * Time.deltaTime;
        forwardMove *= walkSpeed * Time.deltaTime;

        Vector3 movement = transform.right * strafeMove + transform.forward * forwardMove;
        characterController.Move(movement);
    }
}
