using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleArrow : MonoBehaviour {

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        // We get a console warning if Quaternion.LookRotation is called on a zero vector,
        // so we'll check it first.
        if (rb.velocity != Vector3.zero) {
            Quaternion rotationTowardsVelocity = Quaternion.LookRotation(rb.velocity.normalized);
            transform.rotation = rotationTowardsVelocity;
        }
    }
}
