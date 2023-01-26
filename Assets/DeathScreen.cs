using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public GameObject characterBrain;
    public GameObject scoreUI;
   public GameObject interactionUI;
   public GameObject instructionUI;
    public GameObject dialogueUI;


    private void Awake() {
        this.transform.GetChild(0).gameObject.SetActive(false);
        characterBrain = GameObject.Find("CharacterBrain");
        scoreUI = GameObject.Find("Score");
        interactionUI = GameObject.Find("PopUps");
        dialogueUI = GameObject.Find("DialogueBox");

    }

    private void Update() {
        if(PlayerStats.playerIsDead) {
            Invoke(nameof(StartDeathScreen),2f);
        }
    }

    public void StartDeathScreen() {
        this.transform.GetChild(0).gameObject.SetActive(true);
        characterBrain.SetActive(false);
        scoreUI.SetActive(false);
        interactionUI.SetActive(false);
        dialogueUI.SetActive(false);
    }
}
