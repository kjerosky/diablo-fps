using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrighteningState : EnemyBaseState {

    private const float BRIGHTENING_TIME = 1;
    private const float PERCENTAGE_CHANGE_RATE = 1 / BRIGHTENING_TIME;
    private float percentage;

    private Dictionary<Material, Color>.KeyCollection materials;

    public override void enterState(EnemyStateManager manager) {
        Dictionary<Material, Color> materialsToOriginalColor = manager.getMaterialsToOriginalColorDictionary();
        materials = materialsToOriginalColor.Keys;

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
        percentage = Mathf.Min(1, percentage + PERCENTAGE_CHANGE_RATE * Time.deltaTime);
        setPercentageForAllMaterials();

        if (percentage == 1) {
            return EnemyStateTransition.TO_DISSOLVING;
        }

        return EnemyStateTransition.NO_TRANSITION;
    }

    private void setPercentageForAllMaterials() {
        foreach (Material material in materials) {
            material.SetFloat("Percentage", percentage);
        }
    }
}
