using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour {

    public void respawn() {
        GameObject.Find("Player").GetComponent<PlayerLife>().respawn();
    }
}
