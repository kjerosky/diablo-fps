using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour {

    public Shader highlightShader;
    public Shader brighteningShader;

    private Dictionary<Material, Shader> materialsToOriginalShader;
    private Dictionary<Material, Color> materialsToOriginalColor;

    private EnemyBaseState currentState;
    private const EnemyStateTransition INITIAL_TRANSITION = EnemyStateTransition.TO_ALIVE;
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

        transitionsToStateDictionary = new Dictionary<EnemyStateTransition, EnemyBaseState>();
        transitionsToStateDictionary.Add(EnemyStateTransition.NO_TRANSITION, null);
        transitionsToStateDictionary.Add(EnemyStateTransition.TO_ALIVE, new EnemyAliveState());
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
}

public enum EnemyStateTransition {
    NO_TRANSITION,
    TO_ALIVE,
    TO_FALLING,
    TO_BRIGHTENING,
    TO_DISSOLVING
}
