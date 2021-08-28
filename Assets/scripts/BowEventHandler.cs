using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowEventHandler : MonoBehaviour {

    public WeaponManager weaponManager;
    public GameObject arrowPrefab;
    public Transform arrowLaunchPoint;
    public float arrowInitialVelocity = 40;
    public AudioClip bowReadySound;
    public AudioClip bowNockArrowSound;
    public AudioClip bowPullSound;
    public AudioClip bowReleaseSound;

    private AudioSource bowReadyAudioSource;
    private AudioSource bowNockArrowAudioSource;
    private AudioSource bowPullAudioSource;
    private AudioSource bowReleaseAudioSource;

    void Awake() {
        bowReadyAudioSource = gameObject.AddComponent<AudioSource>();
        bowReadyAudioSource.clip = bowReadySound;
        bowReadyAudioSource.playOnAwake = false;
        bowNockArrowAudioSource = gameObject.AddComponent<AudioSource>();
        bowNockArrowAudioSource.clip = bowNockArrowSound;
        bowNockArrowAudioSource.playOnAwake = false;
        bowPullAudioSource = gameObject.AddComponent<AudioSource>();
        bowPullAudioSource.clip = bowPullSound;
        bowPullAudioSource.playOnAwake = false;
        bowReleaseAudioSource = gameObject.AddComponent<AudioSource>();
        bowReleaseAudioSource.clip = bowReleaseSound;
        bowReleaseAudioSource.playOnAwake = false;
    }

    public void SignalBowWasPutAway() {
        weaponManager.switchToNextSelectedWeapon();
    }

    public void FireArrow() {
        GameObject newArrow = Instantiate(arrowPrefab, arrowLaunchPoint.position, arrowLaunchPoint.rotation);

        Vector3 initialVelocity = newArrow.transform.forward * arrowInitialVelocity;

        Rigidbody rb = newArrow.GetComponent<Rigidbody>();
        rb.AddForce(initialVelocity, ForceMode.VelocityChange);

        Quaternion rotationTowardsVelocity = Quaternion.LookRotation(initialVelocity.normalized);
        newArrow.transform.rotation = rotationTowardsVelocity;
    }

    public void playReadySound() {
        bowReadyAudioSource.Play();
    }

    public void playNockArrowSound() {
        bowNockArrowAudioSource.Play();
    }

    public void playPullSound() {
        bowPullAudioSource.Play();
    }

    public void playReleaseSound() {
        bowReleaseAudioSource.Play();
    }
}
