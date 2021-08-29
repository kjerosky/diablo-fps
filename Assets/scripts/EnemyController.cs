using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Health health;
    private Animator animator;

    void Awake() {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
    }

    void LateUpdate() {
        if (health.getPercentage() <= 0) {
            GetComponent<Collider>().enabled = false;
            animator.SetTrigger("Die");
        }
    }
}
