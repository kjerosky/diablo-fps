using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEnemyInfo : MonoBehaviour {

    public PlayerLook playerLook;
    public Text enemyNameText;
    public GameObject enemyHealthBar;

    private Slider slider;
    
    void Start() {
        slider = enemyHealthBar.GetComponent<Slider>();

        enemyNameText.enabled = false;
        enemyHealthBar.SetActive(false);
    }

    void Update() {
        GameObject targetEnemy = playerLook.getTargetEnemy();
        if (targetEnemy == null) {
            enemyNameText.enabled = false;
            enemyHealthBar.SetActive(false);
        } else {
            enemyNameText.text = targetEnemy.name;

            slider.value = targetEnemy.GetComponent<Health>().getPercentage();

            enemyNameText.enabled = true;
            enemyHealthBar.SetActive(true);
        }
    }
}
