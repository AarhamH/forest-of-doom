using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathScreen : MonoBehaviour
{
    public GameObject characterBrain;
    public GameObject scoreUI;
    public GameObject interactionUI;
    public GameObject instructionUI;
    public GameObject dialogueUI;
    public GameObject winnerGate;


    private void Awake() {
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(false);

        characterBrain = GameObject.Find("CharacterBrain");
        scoreUI = GameObject.Find("Score");
        interactionUI = GameObject.Find("PopUps");
        dialogueUI = GameObject.Find("DialogueBox");
        winnerGate = GameObject.Find("WinnerGate");

    }

    private void Update() {
        if(PlayerStats.playerIsDead) {
            Invoke(nameof(StartDeathScreen),2f);
        }

        else if(winnerGate.GetComponent<WinnerGate>().win) {
            Invoke(nameof(StartWinScreen),1f);        }
    }   

    public void StartDeathScreen() {
        AudioManager.Instance.StopMusic("BackgroundMusic");
        this.transform.GetChild(0).gameObject.SetActive(true);
        characterBrain.SetActive(false);
        scoreUI.SetActive(false);
        interactionUI.SetActive(false);
        dialogueUI.SetActive(false);
        Invoke(nameof(GameOver),3f);   
    }

    public void StartWinScreen() {
        AudioManager.Instance.StopMusic("BackgroundMusic");
        this.transform.GetChild(1).gameObject.SetActive(true);
        characterBrain.SetActive(false);
        scoreUI.SetActive(false);
        interactionUI.SetActive(false);
        dialogueUI.SetActive(false);
        Invoke(nameof(DemoScreen),3f);   

    }


    void GameOver() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    void DemoScreen() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
    }
}
