using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEventHandler : MonoBehaviour {

    public Collider swordCollider;

    void Start() {
        swordCollider.enabled = false;
    }

    public void ActivateWeapon() {
        swordCollider.enabled = true;
    }

    public void DeactivateWeapon() {
        swordCollider.enabled = false;
    }
}
