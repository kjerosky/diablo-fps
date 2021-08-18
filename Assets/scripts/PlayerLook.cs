using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour {

    public Transform viewCamera;
    public float mouseSensitivity = 100f;
    public bool invertLook = true;

    private float xRotation = 0;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY * (invertLook ? -1 : 1);
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        viewCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }
}
