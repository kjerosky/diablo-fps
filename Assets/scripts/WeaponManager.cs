using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour {

    public Weapon[] weapons;
    public PlayerLife playerLife;

    private GameInputs gameInputs;
    private bool previousWeaponWasPressed;
    private bool nextWeaponWasPressed;

    private const int NO_INDEX_SELECTED = -1;
    private int currentWeaponIndex;
    private int nextWeaponIndex;

    void Awake() {
        gameInputs = new GameInputs();
        gameInputs.Player.PreviousWeapon.started += context => handlePreviousWeaponPressed();
        gameInputs.Player.NextWeapon.started += context => handleNextWeaponPressed();

        foreach(var weapon in weapons) {
            weapon.gameObject.SetActive(false);
        }

        currentWeaponIndex = 0;
        weapons[currentWeaponIndex].gameObject.SetActive(true);

        nextWeaponIndex = NO_INDEX_SELECTED;
    }

    void OnEnable() {
        gameInputs.Player.Enable();
    }

    void OnDisable() {
        gameInputs.Player.Disable();
    }

    void handlePreviousWeaponPressed() {
        previousWeaponWasPressed = true;
    }

    void handleNextWeaponPressed() {
        nextWeaponWasPressed = true;
    }

    void Update() {
        if (playerLife.isPlayerDead()) {
            weapons[currentWeaponIndex].gameObject.SetActive(false);
            return;
        }

        if (previousWeaponWasPressed) {
            if (nextWeaponIndex == NO_INDEX_SELECTED) {
                weapons[currentWeaponIndex].putAway();
                nextWeaponIndex = currentWeaponIndex;
            }
                
            nextWeaponIndex--;
            if (nextWeaponIndex < 0) {
                nextWeaponIndex = weapons.Length - 1;
            }
        } else if (nextWeaponWasPressed) {
            if (nextWeaponIndex == NO_INDEX_SELECTED) {
                weapons[currentWeaponIndex].putAway();
                nextWeaponIndex = currentWeaponIndex;
            }

            nextWeaponIndex++;
            if (nextWeaponIndex >= weapons.Length) {
                nextWeaponIndex = 0;
            }
        }
    }

    void LateUpdate() {
        resetPressedButtonStatuses();
    }

    private void resetPressedButtonStatuses() {
        previousWeaponWasPressed = false;
        nextWeaponWasPressed = false;
    }

    public void switchToNextSelectedWeapon() {
        weapons[currentWeaponIndex].gameObject.SetActive(false);
        weapons[nextWeaponIndex].gameObject.SetActive(true);

        currentWeaponIndex = nextWeaponIndex;
        nextWeaponIndex = NO_INDEX_SELECTED;
    }

    public void respawn() {
        weapons[currentWeaponIndex].gameObject.SetActive(true);
    }
}
