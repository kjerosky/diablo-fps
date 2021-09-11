using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour {

    public Shader highlightShader;
    public Shader brighteningShader;
    public Shader dissolveShader;
    public Vector3 initialPosition;
    public float patrolRadius = 10;
    public float playerRangeToStartAttacking = 5;

    private Dictionary<Material, Shader> materialsToOriginalShader;
    private Dictionary<Material, Color> materialsToOriginalColor;

    private EnemyBaseState currentState;
    private const EnemyStateTransition INITIAL_TRANSITION = EnemyStateTransition.TO_PATROL_WAITING;
    private Dictionary<EnemyStateTransition, EnemyBaseState> transitionsToStateDictionary;

    void Start() {
        materialsToOriginalShader = new Dictionary<Material, Shader>();
        materialsToOriginalColor = new Dictionary<Material, Color>();
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) {
            foreach (Material material in renderer.materials) {
                materialsToOriginalShader[material] = material.shader;
                materialsToOriginalColor[material] = material.color;
            }
        }

        initialPosition = transform.position;

        transitionsToStateDictionary = new Dictionary<EnemyStateTransition, EnemyBaseState>();
        transitionsToStateDictionary.Add(EnemyStateTransition.NO_TRANSITION, null);
        transitionsToStateDictionary.Add(EnemyStateTransition.TO_PATROLLING, new EnemyPatrollingState());
        transitionsToStateDictionary.Add(EnemyStateTransition.TO_PATROL_WAITING, new EnemyPatrolWaitingState());
        transitionsToStateDictionary.Add(EnemyStateTransition.TO_FOLLOWING_TARGET, new EnemyFollowingTargetState());
        transitionsToStateDictionary.Add(EnemyStateTransition.TO_ATTACKING_TARGET, new EnemyAttackingTargetState());
        transitionsToStateDictionary.Add(EnemyStateTransition.TO_FALLING, new EnemyFallingState());
        transitionsToStateDictionary.Add(EnemyStateTransition.TO_BRIGHTENING, new EnemyBrighteningState());
        transitionsToStateDictionary.Add(EnemyStateTransition.TO_DISSOLVING, new EnemyDissolvingState());

        transitionToState(transitionsToStateDictionary[INITIAL_TRANSITION]);
    }

    void LateUpdate() {
        EnemyStateTransition transition = currentState.updateState();

        EnemyBaseState nextState = transitionsToStateDictionary[transition];
        if (nextState != null) {
            transitionToState(nextState);
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerRangeToStartAttacking);
    }

    private void transitionToState(EnemyBaseState nextState) {
        currentState = nextState;
        currentState.enterState(this);
    }

    public Dictionary<Material, Shader> getMaterialsToOriginalShaderDictionary() {
        return materialsToOriginalShader;
    }

    public Dictionary<Material, Color> getMaterialsToOriginalColorDictionary() {
        return materialsToOriginalColor;
    }

    public void doneAttacking() {
        currentState = transitionsToStateDictionary[EnemyStateTransition.TO_PATROLLING];
        GetComponent<NavMeshAgent>().isStopped = false;
    }
}

public enum EnemyStateTransition {
    NO_TRANSITION,
    TO_PATROLLING,
    TO_PATROL_WAITING,
    TO_FOLLOWING_TARGET,
    TO_ATTACKING_TARGET,
    TO_FALLING,
    TO_BRIGHTENING,
    TO_DISSOLVING
}
