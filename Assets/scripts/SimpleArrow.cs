using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleArrow : MonoBehaviour {

    public GameObject arrowImpactPrefab;

    private Rigidbody rb;

    private const float MAX_ARROW_LIFETIME = 10;
    private float timeAlive;

    void Start() {
        rb = GetComponent<Rigidbody>();

        timeAlive = 0;
    }

    void Update() {
        // We get a console warning if Quaternion.LookRotation is called on a zero vector,
        // so we'll check it first.
        if (rb.velocity != Vector3.zero) {
            Quaternion rotationTowardsVelocity = Quaternion.LookRotation(rb.velocity.normalized);
            transform.rotation = rotationTowardsVelocity;
        }

        timeAlive += Time.deltaTime;
        if (timeAlive >= MAX_ARROW_LIFETIME) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other) {
        //TODO HANDLE THIS WITH TAGS
        if (other.name == "Player") {
            return;
        }

        Instantiate(arrowImpactPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
