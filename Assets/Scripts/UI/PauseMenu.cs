using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public InputAction pause;
    public PlayerInput playerInput;
    public GameObject characterBrain;
    private void Start() {
        this.transform.GetChild(0).gameObject.SetActive(false);
        characterBrain = GameObject.Find("CharacterBrain");
        playerInput = GetComponent<PlayerInput>();
        pause = playerInput.actions["Pause"];        
        Resume();
    }

    private void Update() {
        if(pause.triggered) {
           if(!this.transform.GetChild(0).gameObject.active) {
            Debug.Log("Pressed");
            Pause();
           }

           else{
            Resume();           
            }
        }

    }   

    public void Pause() {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        characterBrain.SetActive(false);
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Resume() {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        characterBrain.SetActive(true);
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
