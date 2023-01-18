using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AmmoBar : MonoBehaviour
{
    [SerializeField]
    private Image foreground;

    private float reduceSpeed = 1f;
    private float target = 1f;

    public void UpdateHealth(float max, float current) {
        target = current/max;
    }

    private void Update() {
        foreground.fillAmount = Mathf.MoveTowards(foreground.fillAmount,target,reduceSpeed * Time.deltaTime);
    }
}
