using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour {

    public Transform viewCamera;
    public float mouseSensitivity = 100f;
    public bool invertLook = true;

    private float xRotation = 0;

    private const float RECOGNIZE_ENEMY_DISTANCE = 25.0f;
    private int enemiesLayerMask;

    private GameObject targetEnemy;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;

        enemiesLayerMask = LayerMask.GetMask("Enemies");
    }
    
    void Update() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY * (invertLook ? -1 : 1);
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        viewCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);

        checkIfPointingAtEnemy();
    }

    private void checkIfPointingAtEnemy() {
        RaycastHit hitInfo;
        if (Physics.Raycast(viewCamera.position, viewCamera.forward, out hitInfo, RECOGNIZE_ENEMY_DISTANCE, enemiesLayerMask)) {
            targetEnemy = hitInfo.collider.gameObject;
        } else {
            targetEnemy = null;
        }

        //TODO REMOVE THIS!!!
        if (targetEnemy != null && Input.GetKeyDown(KeyCode.Space)) {
            targetEnemy.GetComponent<Health>().takeDamage(10);
        }
    }

    public GameObject getTargetEnemy() {
        return targetEnemy;
    }
}
