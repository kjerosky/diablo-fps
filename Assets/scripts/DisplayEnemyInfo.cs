using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEnemyInfo : MonoBehaviour {

    public PlayerLook playerLook;
    public Text enemyNameText;
    
    void Start() {
        enemyNameText.text = "";
    }

    void Update() {
        GameObject targetEnemy = playerLook.getTargetEnemy();
        if (targetEnemy == null) {
            enemyNameText.text = "";
        } else {
            enemyNameText.text = targetEnemy.name;
        }
    }
}
