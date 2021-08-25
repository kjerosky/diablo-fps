using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public Weapon[] weapons;

    private const int NO_INDEX_SELECTED = -1;
    private int currentWeaponIndex;
    private int nextWeaponIndex;

    void Start() {
        foreach(var weapon in weapons) {
            weapon.gameObject.SetActive(false);
        }

        currentWeaponIndex = 0;
        weapons[currentWeaponIndex].gameObject.SetActive(true);

        nextWeaponIndex = NO_INDEX_SELECTED;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftBracket)) {
            if (nextWeaponIndex == NO_INDEX_SELECTED) {
                weapons[currentWeaponIndex].putAway();
                nextWeaponIndex = currentWeaponIndex;
            }
                
            nextWeaponIndex--;
            if (nextWeaponIndex < 0) {
                nextWeaponIndex = weapons.Length - 1;
            }
        } else if (Input.GetKeyDown(KeyCode.RightBracket)) {
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

    public void switchToNextSelectedWeapon() {
        weapons[currentWeaponIndex].gameObject.SetActive(false);
        weapons[nextWeaponIndex].gameObject.SetActive(true);

        currentWeaponIndex = nextWeaponIndex;
        nextWeaponIndex = NO_INDEX_SELECTED;
    }
}
