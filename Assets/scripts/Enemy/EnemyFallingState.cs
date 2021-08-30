using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFallingState : EnemyBaseState {

    private const float TIME_TO_TRANSITION = 5;
    private float timeLeftInThisState;

    public override void enterState(EnemyStateManager manager) {
        manager.GetComponent<Animator>().SetTrigger("Die");
        manager.GetComponent<Collider>().enabled = false;

        timeLeftInThisState = TIME_TO_TRANSITION;
    }

    public override EnemyStateTransition updateState() {
        timeLeftInThisState -= Time.deltaTime;
        if (timeLeftInThisState <= 0) {
            return EnemyStateTransition.TO_BRIGHTENING;
        }

        return EnemyStateTransition.NO_TRANSITION;
    }
}
