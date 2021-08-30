using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Shader highlightShader;

    private PlayerLook playerLook;
    private Health health;
    private Animator animator;
    private bool isDead;

    private Dictionary<Material, Shader> materialsToOriginalShader;
    private Dictionary<Material, Color> materialsToOriginalColor;
    private bool isHighlighted;

    void Awake() {
        playerLook = GameObject.Find("Player").GetComponent<PlayerLook>();
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();

        isDead = false;

        isHighlighted = false;
        materialsToOriginalShader = new Dictionary<Material, Shader>();
        materialsToOriginalColor = new Dictionary<Material, Color>();
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) {
            foreach (Material material in renderer.materials) {
                materialsToOriginalShader[material] = material.shader;
                materialsToOriginalColor[material] = material.color;
            }
        }
    }

    void LateUpdate() {
        if (!isDead && health.getPercentage() <= 0) {
            GetComponent<Collider>().enabled = false;

            animator.SetTrigger("Die");

            removeHighlight();

            isDead = true;
        }

        GameObject enemyBeingLookedAtByPlayer = playerLook.getTargetEnemy();
        if (!isHighlighted && !isDead && gameObject == enemyBeingLookedAtByPlayer) {
            addHighlight();
        } else if (isHighlighted && (isDead || gameObject != enemyBeingLookedAtByPlayer)) {
            removeHighlight();
        }
    }

    private void addHighlight() {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) {
            foreach (Material material in renderer.materials) {
                material.shader = highlightShader;
                material.SetColor("Base_Color", materialsToOriginalColor[material]);
            }
        }

        isHighlighted = true;
    }

    private void removeHighlight() {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) {
            foreach (Material material in renderer.materials) {
                material.shader = materialsToOriginalShader[material];
            }
        }

        isHighlighted = false;
    }
}
