using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowingTargetState : EnemyBaseState {

    private GameObject player;
    private GameObject thisEnemy;
    private Health health;
    private Shader highlightShader;
    private PlayerLook playerLook;
    private NavMeshAgent navMeshAgent;

    private const float PLAYER_RANGE_TO_ATTACK = 2.5f;
    private const float MAX_ANGLE_TO_PLAYER_FOR_ATTACKING = 10;
    private const float TURN_TOWARDS_PLAYER_RATE = 180;
    private const float TIME_TO_DISENGAGE = 5;
    private float timeLeftToDisengage;

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

        timeLeftToDisengage = TIME_TO_DISENGAGE;
        navMeshAgent.isStopped = false;

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

        if (player.GetComponent<PlayerLife>().isPlayerDead()) {
            return EnemyStateTransition.TO_PATROLLING;
        }

        timeLeftToDisengage -= Time.deltaTime;
        if (timeLeftToDisengage <= 0) {
            return EnemyStateTransition.TO_PATROLLING;
        }

        if (Vector3.Distance(thisEnemy.transform.position, player.transform.position) > PLAYER_RANGE_TO_ATTACK) {
            navMeshAgent.SetDestination(player.transform.position);
        } else {
            Vector3 myXZDirection = new Vector3(thisEnemy.transform.forward.x, 0, thisEnemy.transform.forward.z).normalized;
            Vector3 toPlayerXZDirection = player.transform.position - thisEnemy.transform.position;
            toPlayerXZDirection.y = 0;
            toPlayerXZDirection = toPlayerXZDirection.normalized;
            if (Vector3.Angle(myXZDirection, toPlayerXZDirection) > MAX_ANGLE_TO_PLAYER_FOR_ATTACKING) {
                Quaternion incrementalTowardsPlayerRotation = Quaternion.RotateTowards(
                    Quaternion.LookRotation(myXZDirection),
                    Quaternion.LookRotation(toPlayerXZDirection),
                    TURN_TOWARDS_PLAYER_RATE * Time.deltaTime
                );
                thisEnemy.transform.rotation = incrementalTowardsPlayerRotation;
            } else {
                return EnemyStateTransition.TO_ATTACKING_TARGET;
            }
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
