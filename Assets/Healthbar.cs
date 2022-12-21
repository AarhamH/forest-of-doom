using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField]
    private Image foreground;
    [SerializeField]
    private Camera cam;

    private float reduceSpeed = 1f;
    private float target = 1f;

    private void Start() {
        cam = Camera.main;
    }
    // Start is called before the first frame update
    public void UpdateHealthBar(float maxHealth, float currentHealth) {
        target = currentHealth/maxHealth;
    }

    private void Update() {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        foreground.fillAmount = Mathf.MoveTowards(foreground.fillAmount,target,reduceSpeed * Time.deltaTime);
    }
}
