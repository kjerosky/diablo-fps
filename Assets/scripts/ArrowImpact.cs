using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowImpact : MonoBehaviour {

    private ParticleSystem sparks;
    private AudioSource hitSound;

    void Start() {
        sparks = GetComponent<ParticleSystem>();
        hitSound = GetComponent<AudioSource>();
    }

    void Update() {
        if (sparks.isStopped && !hitSound.isPlaying) {
            Destroy(gameObject);
        }
    }
}
