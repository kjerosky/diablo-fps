using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBow : MonoBehaviour {

    public GameObject arrowPrefab;
    public Transform arrowLaunchPoint;
    public float arrowInitialVelocity = 1;

    void Update() {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.J)) {
            GameObject newArrow = Instantiate(arrowPrefab, arrowLaunchPoint.position, arrowLaunchPoint.rotation);

            Vector3 initialVelocity = newArrow.transform.forward * arrowInitialVelocity;

            Rigidbody rb = newArrow.GetComponent<Rigidbody>();
            rb.AddForce(initialVelocity, ForceMode.VelocityChange);

            Quaternion rotationTowardsVelocity = Quaternion.LookRotation(initialVelocity.normalized);
            newArrow.transform.rotation = rotationTowardsVelocity;
        }
    }
}
