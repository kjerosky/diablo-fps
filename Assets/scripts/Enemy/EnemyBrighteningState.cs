using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrighteningState : EnemyBaseState {

    private const float BRIGHTENING_TIME = 2;
    private const float PERCENTAGE_CHANGE_RATE = 1 / BRIGHTENING_TIME;
    private float percentage;

    private Dictionary<Material, Color> materialsToOriginalColor;

    public override void enterState(EnemyStateManager manager) {
        materialsToOriginalColor = manager.getMaterialsToOriginalColorDictionary();
        foreach (KeyValuePair<Material, Color> entry in materialsToOriginalColor) {
            Material material = entry.Key;
            material.shader = manager.brighteningShader;
            material.SetColor("Start_Color", entry.Value);
            material.SetColor("End_Color", Color.white);
            material.SetFloat("Percentage", 0);
        }

        percentage = 0;
    }

    public override EnemyStateTransition updateState() {
        percentage += PERCENTAGE_CHANGE_RATE * Time.deltaTime;
        if (percentage >= 1) {
            percentage = 1;
            setPercentageForAllMaterials();

            return EnemyStateTransition.TO_DISSOLVING;
        }

        setPercentageForAllMaterials();

        return EnemyStateTransition.NO_TRANSITION;
    }

    private void setPercentageForAllMaterials() {
        foreach (Material material in materialsToOriginalColor.Keys) {
                material.SetFloat("Percentage", percentage);
        }
    }
}
