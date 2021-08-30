using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAliveState : EnemyBaseState {

    private GameObject thisEnemy;
    private Health health;
    private Shader highlightShader;
    private PlayerLook playerLook;

    private Dictionary<Material, Shader> materialsToOriginalShader;
    private Dictionary<Material, Color> materialsToOriginalColor;
    private bool isHighlighted;

    public override void enterState(EnemyStateManager manager) {
        thisEnemy = manager.gameObject;
        health = manager.GetComponent<Health>();
        highlightShader = manager.highlightShader;
        playerLook = GameObject.Find("Player").GetComponent<PlayerLook>();

        isHighlighted = false;
        materialsToOriginalShader = manager.getMaterialsToOriginalShaderDictionary();
        materialsToOriginalColor = manager.getMaterialsToOriginalColorDictionary();
    }

    public override EnemyStateTransition updateState() {
        GameObject enemyBeingLookedAtByPlayer = playerLook.getTargetEnemy();
        if (thisEnemy == enemyBeingLookedAtByPlayer) {
            addHighlight();
        } else {
            removeHighlight();
        }

        if (health.getPercentage() == 0) {
            removeHighlight();
            return EnemyStateTransition.TO_FALLING;
        }

        return EnemyStateTransition.NO_TRANSITION;
    }

    private void addHighlight() {
        if (isHighlighted) {
            return;
        }

        foreach (KeyValuePair<Material, Color> entry in materialsToOriginalColor) {
            Material material = entry.Key;
            material.shader = highlightShader;
            material.SetColor("Base_Color", entry.Value);
        }

        isHighlighted = true;
    }

    private void removeHighlight() {
        if (!isHighlighted) {
            return;
        }

        foreach (KeyValuePair<Material, Shader> entry in materialsToOriginalShader) {
            entry.Key.shader = entry.Value;
        }

        isHighlighted = false;
    }
}
