using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEventHandler : MonoBehaviour {

    public Collider swordCollider;
    public WeaponManager weaponManager;
    public AudioClip swordUnsheathSound;
    public AudioClip swordSwingSound;
    public AudioClip swordSheathSound;

    private AudioSource swordUnsheathAudioSource;
    private AudioSource swordSwingAudioSource;
    private AudioSource swordSheathAudioSource;

    void Awake() {
        swordCollider.enabled = false;

        swordUnsheathAudioSource = gameObject.AddComponent<AudioSource>();
        swordUnsheathAudioSource.clip = swordUnsheathSound;
        swordUnsheathAudioSource.playOnAwake = false;
        swordSwingAudioSource = gameObject.AddComponent<AudioSource>();
        swordSwingAudioSource.clip = swordSwingSound;
        swordSwingAudioSource.playOnAwake = false;
        swordSheathAudioSource = gameObject.AddComponent<AudioSource>();
        swordSheathAudioSource.clip = swordSheathSound;
        swordSheathAudioSource.playOnAwake = false;
        swordSheathAudioSource.volume = 0.5f;
    }

    public void ActivateWeapon() {
        swordCollider.enabled = true;
        playSwingSound();
    }

    public void DeactivateWeapon() {
        swordCollider.enabled = false;
    }

    public void SignalSwordWasPutAway() {
        weaponManager.switchToNextSelectedWeapon();
    }

    public void playUnsheathSound() {
        if (swordUnsheathAudioSource != null) {
            swordUnsheathAudioSource.Play();
        }
    }

    public void playSwingSound() {
        swordSwingAudioSource.Play();
    }

    public void playSheathSound() {
        swordSheathAudioSource.Play();
    }
}
