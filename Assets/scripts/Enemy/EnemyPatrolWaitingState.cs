using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolWaitingState : EnemyBaseState {

    private GameObject player;
    private GameObject thisEnemy;
    private Health health;
    private Shader highlightShader;
    private PlayerLook playerLook;
    private NavMeshAgent navMeshAgent;

    private Vector3 initialPosition;
    private float patrolRadius;
    private float playerRangeToStartAttacking;
    private float timeLeftUntilMove;
    private const float WAIT_TIME_UNTIL_NEXT_MOVE = 2;

    private Dictionary<Material, Shader> materialsToOriginalShader;
    private Dictionary<Material, Color> materialsToOriginalColor;
    private bool isHighlighted;

    public override void enterState(EnemyStateManager manager) {
        player = GameObject.Find("Player");
        thisEnemy = manager.gameObject;
        health = manager.GetComponent<Health>();
        highlightShader = manager.highlightShader;
        playerLook = player.GetComponent<PlayerLook>();
        navMeshAgent = manager.GetComponent<NavMeshAgent>();

        initialPosition = manager.initialPosition;
        patrolRadius = manager.patrolRadius;
        playerRangeToStartAttacking = manager.playerRangeToStartAttacking;
        timeLeftUntilMove = WAIT_TIME_UNTIL_NEXT_MOVE;

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
            navMeshAgent.enabled = false;
            return EnemyStateTransition.TO_FALLING;
        }

        timeLeftUntilMove -= Time.deltaTime;
        if (timeLeftUntilMove <= 0) {
            return EnemyStateTransition.TO_PATROLLING;
        } else if (Vector3.Distance(thisEnemy.transform.position, player.transform.position) <= playerRangeToStartAttacking) {
            return EnemyStateTransition.TO_FOLLOWING_TARGET;
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
