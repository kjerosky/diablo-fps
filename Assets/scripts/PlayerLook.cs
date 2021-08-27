using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour {

    public Transform viewCamera;
    public float mouseSensitivity = 100f;
    public bool invertLook = true;

    private GameInputs gameInputs;
    private Vector2 currentLook;

    private float xRotation = 0;

    private const float RECOGNIZE_ENEMY_DISTANCE = 25.0f;
    private int enemiesLayerMask;

    private GameObject targetEnemy;

    void Awake() {
        gameInputs = new GameInputs();
        gameInputs.Player.Look.started += handleLook;
        gameInputs.Player.Look.performed += handleLook;
        gameInputs.Player.Look.canceled += handleLook;

        Cursor.lockState = CursorLockMode.Locked;

        enemiesLayerMask = LayerMask.GetMask("Enemies");
    }

    void OnEnable() {
        gameInputs.Player.Enable();
    }

    void OnDisable() {
        gameInputs.Player.Disable();
    }

    private void handleLook(InputAction.CallbackContext context) {
        currentLook = context.ReadValue<Vector2>();
        if (invertLook) {
            currentLook.y *= -1;
        }
    }
    
    void Update() {
        float horizontalLook = currentLook.x * mouseSensitivity * Time.deltaTime;
        float verticalLook = currentLook.y * mouseSensitivity * Time.deltaTime;

        xRotation -= verticalLook;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        viewCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * horizontalLook);

        checkIfPointingAtEnemy();
    }

    private void checkIfPointingAtEnemy() {
        RaycastHit hitInfo;
        if (Physics.Raycast(viewCamera.position, viewCamera.forward, out hitInfo, RECOGNIZE_ENEMY_DISTANCE, enemiesLayerMask)) {
            targetEnemy = hitInfo.collider.gameObject;
        } else {
            targetEnemy = null;
        }
    }

    public GameObject getTargetEnemy() {
        return targetEnemy;
    }
}
