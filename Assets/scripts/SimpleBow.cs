using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBow : Weapon {

    public Animator animator;

    void Update() {
        bool firePressed = Input.GetMouseButton(0) || Input.GetKey(KeyCode.J);
        animator.SetBool("FirePressed", firePressed);
    }

    public override void putAway() {
        animator.SetTrigger("PutAway");
    }
}
