using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleBow : Weapon {

    public Animator animator;

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

        // Cause the animator to update immediately, otherwise the prefab in its default
        // position will show for the first frame the first time you switch to this weapon.
        animator.Update(0);
    }

    void OnDisable() {
        gameInputs.Player.Disable();
    }

    void handleAttack(InputAction.CallbackContext context) {
        attackIsPressed = context.ReadValueAsButton();
    }

    void Update() {
        animator.SetBool("FirePressed", attackIsPressed);
    }

    public override void putAway() {
        animator.SetTrigger("PutAway");
    }
}
