using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSword : Weapon {

    public Animator animator;
    public Collider swordCollisionVolume;

    void Update() {
        //TODO REMOVE THE KEYBOARD DEBUG HERE!!
        bool attackIsPressed = Input.GetMouseButton(0) || Input.GetKey(KeyCode.J);
        
        if (attackIsPressed && animator.GetCurrentAnimatorStateInfo(0).IsName("swordIdle")) {
            animator.SetTrigger("DoAttack");
        }
    }

    void OnDisable() {
        // Since the sword collision volume is enabled/disabled using animation events,
        // if the player switches weapons before the disabling happens, then the sword
        // collision volume will stay enabled.  This stops that situation from happening.
        swordCollisionVolume.enabled = false;
    }

    public override void putAway() {
        animator.SetTrigger("PutAway");
    }
}
