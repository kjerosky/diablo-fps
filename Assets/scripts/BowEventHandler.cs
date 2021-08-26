using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowEventHandler : MonoBehaviour {

    public WeaponManager weaponManager;
    public GameObject arrowPrefab;
    public Transform arrowLaunchPoint;
    public float arrowInitialVelocity = 40;

    public void SignalBowWasPutAway() {
        weaponManager.switchToNextSelectedWeapon();
    }

    public void FireArrow() {
        GameObject newArrow = Instantiate(arrowPrefab, arrowLaunchPoint.position, arrowLaunchPoint.rotation);

        Vector3 initialVelocity = newArrow.transform.forward * arrowInitialVelocity;

        Rigidbody rb = newArrow.GetComponent<Rigidbody>();
        rb.AddForce(initialVelocity, ForceMode.VelocityChange);

        Quaternion rotationTowardsVelocity = Quaternion.LookRotation(initialVelocity.normalized);
        newArrow.transform.rotation = rotationTowardsVelocity;
    }
}
