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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void PlayAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }

    public void Menu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-2);
    }

    public void MenuFromDemo() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-3);

    }

    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }

}
