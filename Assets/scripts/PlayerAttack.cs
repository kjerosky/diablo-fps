using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public Animator animator;

    void Update() {
        //TODO REMOVE THE KEYBOARD DEBUG HERE!!
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.J)) && animator.GetCurrentAnimatorStateInfo(0).IsName("swordIdle")) {
            animator.SetTrigger("DoAttack");
        }
    }
}
