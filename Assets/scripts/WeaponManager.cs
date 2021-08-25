using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public GameObject[] weapons;

    private int currentWeaponIndex;

    void Start() {
        foreach(var weapon in weapons) {
            weapon.SetActive(false);
        }

        currentWeaponIndex = 0;
        weapons[currentWeaponIndex].SetActive(true);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftBracket)) {
            weapons[currentWeaponIndex].SetActive(false);
            
            currentWeaponIndex--;
            if (currentWeaponIndex < 0) {
                currentWeaponIndex = weapons.Length - 1;
            }
            
            weapons[currentWeaponIndex].SetActive(true);
        } else if (Input.GetKeyDown(KeyCode.RightBracket)) {
            weapons[currentWeaponIndex].SetActive(false);

            currentWeaponIndex++;
            if (currentWeaponIndex >= weapons.Length) {
                currentWeaponIndex = 0;
            }

            weapons[currentWeaponIndex].SetActive(true);
        }
    }
}
