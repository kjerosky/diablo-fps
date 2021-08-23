using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int maxHealth = 100;

    private int currentHealth;

    void Start() {
        currentHealth = maxHealth;
    }

    public void takeDamage(int damageAmount) {
        currentHealth = Mathf.Max(0, currentHealth - 10);
    }

    public float getPercentage() {
        return (float)currentHealth / maxHealth;
    }
}
