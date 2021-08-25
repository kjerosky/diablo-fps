using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSword : MonoBehaviour {

    public Animator animator;

    void Update() {
        //TODO REMOVE THE KEYBOARD DEBUG HERE!!
        bool attackIsPressed = Input.GetMouseButton(0) || Input.GetKey(KeyCode.J);
        
        if (attackIsPressed && animator.GetCurrentAnimatorStateInfo(0).IsName("swordIdle")) {
            animator.SetTrigger("DoAttack");
        }
    }
}
