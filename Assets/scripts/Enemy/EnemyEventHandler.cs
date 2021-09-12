using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEventHandler : MonoBehaviour {

    public Collider attackCollisionVolume;

    public void enableAttackCollisionVolume() {
        attackCollisionVolume.enabled = true;
    }

    public void disableAttackCollisionVolume() {
        attackCollisionVolume.enabled = false;
    }
}
