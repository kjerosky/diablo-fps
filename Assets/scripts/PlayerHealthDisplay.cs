using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour {

    public Health playerHealth;
    public Slider playerHealthBarSlider;

    void Update() {
        playerHealthBarSlider.value = playerHealth.getPercentage();
    }
}
