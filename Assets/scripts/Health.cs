using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float maxHealth = 100;

    private float currentHealth;

    void Start() {
        respawn();
    }

    public void respawn() {
        currentHealth = maxHealth;
    }

    void OnTriggerEnter(Collider other) {
        bool playerWeaponHitEnemy = tag == "Enemy" && other.tag == "PlayerWeapon";
        bool enemyWeaponHitPlayer = tag == "Player" && other.tag == "EnemyWeapon";
        if (!playerWeaponHitEnemy && !enemyWeaponHitPlayer) {
            return;
        }

        takeDamage(10);  //TODO SET THIS TO THE WEAPON'S DAMAGE!!
    }

    public void takeDamage(int damageAmount) {
        currentHealth = Mathf.Max(0, currentHealth - damageAmount);
    }

    public float getPercentage() {
        return currentHealth / maxHealth;
    }
}
