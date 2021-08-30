using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDissolvingState : EnemyBaseState {

    private const float DISSOLVE_TIME = 2;
    private const float PERCENTAGE_CHANGE_RATE = 1 / DISSOLVE_TIME;
    private float percentage;

    private GameObject thisEnemy;
    private Dictionary<Material, Color>.KeyCollection materials;

    public override void enterState(EnemyStateManager manager) {
        thisEnemy = manager.gameObject;

        materials = manager.getMaterialsToOriginalColorDictionary().Keys;

        percentage = 0;
        foreach (Material material in materials) {
            material.shader = manager.dissolveShader;
            material.SetColor("baseColor", Color.white);
            material.SetFloat("percentage", percentage);
        }
    }

    public override EnemyStateTransition updateState() {
        percentage = Mathf.Min(1, percentage + PERCENTAGE_CHANGE_RATE * Time.deltaTime);
        setPercentageForAllMaterials();

        if (percentage == 1) {
            GameObject.Destroy(thisEnemy);
        }

        return EnemyStateTransition.NO_TRANSITION;
    }

    private void setPercentageForAllMaterials() {
        foreach (Material material in materials) {
            material.SetFloat("percentage", percentage);
        }
    }
}
