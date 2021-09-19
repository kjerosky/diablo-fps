using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour {

    private Health health;
    private Animator animator;

    private bool isDead;

    void Awake() {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();

        isDead = false;
    }

    public bool isPlayerDead() {
        return isDead;
    }

    void Update() {
        if (health.getPercentage() > 0f) {
            return;
        }

        isDead = true;
        animator.SetTrigger("die");
    }
}
