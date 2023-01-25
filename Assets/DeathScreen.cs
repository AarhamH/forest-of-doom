using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{

    private void Awake() {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Update() {
        if(PlayerStats.playerIsDead) {
            Invoke(nameof(StartDeathScreen),2f);
        }
    }

    public void StartDeathScreen() {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }
}
