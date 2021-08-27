using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int maxHealth = 100;

    private int currentHealth;

    void Start() {
        currentHealth = maxHealth;
    }

    void OnTriggerEnter(Collider other) {
        takeDamage(10);  //TODO SET THIS TO THE WEAPON'S DAMAGE!!
    }

    public void takeDamage(int damageAmount) {
        currentHealth = Mathf.Max(0, currentHealth - damageAmount);
    }

    public float getPercentage() {
        return (float)currentHealth / maxHealth;
    }
}
