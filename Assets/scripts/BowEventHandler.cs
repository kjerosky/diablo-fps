using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowEventHandler : MonoBehaviour {

    public WeaponManager weaponManager;

    public void SignalBowWasPutAway() {
        weaponManager.switchToNextSelectedWeapon();
    }
}
