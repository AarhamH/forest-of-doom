using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainHealth : MonoBehaviour
{
    [SerializeField]
    private Image foreground;

    private float reduceSpeed = 1f;
    private float target = 1f;
    bool lowHealth;

    public void UpdateHealth(float maxHealth, float currentHealth) {
        target = currentHealth/maxHealth;
        if(currentHealth < 50f) {
            lowHealth = true;
        }
        else {
            lowHealth = false;
        }
    }

    private void Update() {

        if(lowHealth) {
            foreground.color = Color.red;
        }

        else{
            foreground.color = Color.green;
        }
        foreground.fillAmount = Mathf.MoveTowards(foreground.fillAmount,target,reduceSpeed * Time.deltaTime);
    }
}
