using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStamina : MonoBehaviour {

    public GameObject staminaIcon;
    public GameObject staminaBar;
    public Stamina stamina;
    public Slider slider;
    public Color normalStaminaColor;
    public Color exhaustedStaminaColor;
    public Image barFillImage;

    void Awake() {
        staminaIcon.SetActive(false);
        staminaBar.SetActive(false);
    }

    void Update() {
        if (stamina.isAtMaxStamina()) {
            staminaIcon.SetActive(false);
            staminaBar.SetActive(false);
            return;
        }

        Color barFillColor = stamina.canUseStamina() ? normalStaminaColor : exhaustedStaminaColor;
        barFillImage.color = barFillColor;

        slider.value = stamina.getStaminaRemaining();

        staminaIcon.SetActive(true);
        staminaBar.SetActive(true);

    }
}
