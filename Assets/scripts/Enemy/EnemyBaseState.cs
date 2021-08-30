using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState {

    public abstract void enterState(EnemyStateManager manager);

    public abstract EnemyStateTransition updateState();
}
