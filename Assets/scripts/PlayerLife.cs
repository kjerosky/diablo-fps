using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour {

    public Animator transitionAnimator;

    private Health health;
    private Animator animator;
    private CharacterController characterController;

    private bool isDead;

    void Awake() {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

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

    public void finishedDying() {
        transitionAnimator.SetTrigger("playerFinishedDying");
    }

    public void respawn() {
        animator.ResetTrigger("die");
        transitionAnimator.ResetTrigger("playerFinishedDying");

        isDead = false;
        health.respawn();
        animator.SetTrigger("respawn");

        GameObject.Find("Weapons").GetComponent<WeaponManager>().respawn();

        characterController.enabled = false;
        GameObject respawnPoint = GameObject.Find("RespawnPoint");
        transform.position = respawnPoint.transform.position;
        transform.rotation = respawnPoint.transform.rotation;
        GetComponent<PlayerLook>().respawn();
        characterController.enabled = true;

        transitionAnimator.SetTrigger("respawn");
    }
}
