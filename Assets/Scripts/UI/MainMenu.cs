using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;   
    }
    public void PlayGame() {
        SceneManager.LoadScene("Forest");
    }

    public void Menu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }

}
