using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour {

    public float staminaRechargeRate = 25;
    public float sprintingStaminaDecreaseRate = 20;
    public float dashingStaminaDecreaseAmount = 50;

    private const float MAX_STAMINA = 100;
    private const float EXHAUSTION_RECOVERY_THRESHOLD = 50;

    private float staminaRemaining;
    private bool isExhausted;
    private bool staminaWasUsed;

    void Awake() {
        staminaRemaining = MAX_STAMINA;
        isExhausted = false;
        staminaWasUsed = false;
    }

    void LateUpdate() {
        if (staminaWasUsed) {
            staminaWasUsed = false;
            return;
        }

        staminaRemaining = Mathf.Min(MAX_STAMINA, staminaRemaining + staminaRechargeRate * Time.deltaTime);
        if (staminaRemaining >= EXHAUSTION_RECOVERY_THRESHOLD) {
            isExhausted = false;
        }

        staminaWasUsed = false;
    }

    public void useStamina(StaminaType staminaType) {
        if (!canUseStamina()) {
            return;
        }

        float staminaToUse = 0;
        if (staminaType == StaminaType.SPRINTING) {
            staminaToUse = sprintingStaminaDecreaseRate * Time.deltaTime;
        } else if (staminaType == StaminaType.DASHING) {
            staminaToUse = dashingStaminaDecreaseAmount;
        }

        staminaRemaining = Mathf.Max(0, staminaRemaining - staminaToUse);
        isExhausted = staminaRemaining == 0;
        staminaWasUsed = true;
    }

    public bool canUseStamina() {
        return !isExhausted;
    }

    public float getStaminaRemaining() {
        return staminaRemaining;
    }

    public bool isAtMaxStamina() {
        return staminaRemaining == MAX_STAMINA;
    }
}

public enum StaminaType {
    SPRINTING,
    DASHING
}