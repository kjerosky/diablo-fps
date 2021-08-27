using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleSword : Weapon {

    public Animator animator;
    public Collider swordCollisionVolume;

    private GameInputs gameInputs;
    private bool attackIsPressed;

    void Awake() {
        gameInputs = new GameInputs();
        gameInputs.Player.Attack.started += handleAttack;
        gameInputs.Player.Attack.performed += handleAttack;
        gameInputs.Player.Attack.canceled += handleAttack;
    }

    void OnEnable() {
        gameInputs.Player.Enable();

        animator.Update(0);
    }

    void OnDisable() {
        gameInputs.Player.Disable();

        // Since the sword collision volume is enabled/disabled using animation events,
        // if the player switches weapons before the disabling happens, then the sword
        // collision volume will stay enabled.  This stops that situation from happening.
        swordCollisionVolume.enabled = false;
    }

    void handleAttack(InputAction.CallbackContext context) {
        attackIsPressed = context.ReadValueAsButton();
    }

    void Update() {
        if (attackIsPressed && animator.GetCurrentAnimatorStateInfo(0).IsName("swordIdle")) {
            animator.SetTrigger("DoAttack");
        }
    }

    public override void putAway() {
        animator.SetTrigger("PutAway");
    }
}
