using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour {

    private AudioSource deathSound;

    void Awake() {
        deathSound = GetComponent<AudioSource>();
    }

    public void respawn() {
        GameObject.Find("Player").GetComponent<PlayerLife>().respawn();
    }

    public void playDeathSound() {
        deathSound.Play();
    }
}
