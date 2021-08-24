using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEventHandler : MonoBehaviour {

    private BoxCollider boxCollider;

    void Start() {
        boxCollider = GetComponent<BoxCollider>();
        
        boxCollider.enabled = false;
    }

    public void ActivateWeapon() {
        boxCollider.enabled = true;
    }

    public void DeactivateWeapon() {
        boxCollider.enabled = false;
    }
}
