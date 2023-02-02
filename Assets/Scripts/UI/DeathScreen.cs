using UnityEngine;
using UnityEngine.SceneManagement;

/*
    DeathScreen Class: Handles Win and Lose screens when the player wins/loses

    Necessary Components: All UI components (interactionUI, instructionUI, etc)
*/
public class DeathScreen : MonoBehaviour
{
    [Header("UI")]
    private GameObject characterBrain;
    private GameObject scoreUI;
    private GameObject interactionUI;
    private GameObject instructionUI;
    private GameObject dialogueUI;

    [Header("Winner Gate")]
    private GameObject winnerGate;


    /*
        Awake Function: - Disables win and lose screens and finds the UI/Gate components
    */
    private void Awake() {
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(false);

        characterBrain = GameObject.Find("CharacterBrain");
        scoreUI = GameObject.Find("Score");
        interactionUI = GameObject.Find("PopUps");
        dialogueUI = GameObject.Find("DialogueBox");
        winnerGate = GameObject.Find("WinnerGate");
    }


    /*
        Update Function: - If player dies, lose screen is laid out, if player interacts with winner
                           gate, the win screen is laid out
    */
    private void Update() 
    {
        if(PlayerStats.playerIsDead) {
            Invoke(nameof(StartDeathScreen),2f);
        }

        else if(winnerGate.GetComponent<WinnerGate>().win) {
            Invoke(nameof(StartWinScreen),1f);        }
    }   


    /*
        Input: n/A
    
        Functionality: - Method to play lose screen when player dies
                       - Disables all UI components (ex. health bar, score) and enables death screen
                       - Transitions to GameOver scene in a delayed time

        Called In: Update()
    */
    public void StartDeathScreen() 
    {
        AudioManager.Instance.StopMusic("BackgroundMusic");
        this.transform.GetChild(0).gameObject.SetActive(true);
        characterBrain.SetActive(false);
        scoreUI.SetActive(false);
        interactionUI.SetActive(false);
        dialogueUI.SetActive(false);
        Invoke(nameof(GameOver),3f);   
    }


    /*
        Input: n/A
    
        Functionality: - Method to play win screen when player win
                       - Disables all UI components (ex. health bar, score) and enables wib screen
                       - Transitions to "DEMO" scene in a delayed time ( will go to second level when more levels created )

        Called In: Update()
    */
    public void StartWinScreen() 
    {
        AudioManager.Instance.StopMusic("BackgroundMusic");
        this.transform.GetChild(1).gameObject.SetActive(true);
        characterBrain.SetActive(false);
        scoreUI.SetActive(false);
        interactionUI.SetActive(false);
        dialogueUI.SetActive(false);
        Invoke(nameof(DemoScreen),3f);   

    }


    // helper method to transtion scene to game over
    void GameOver() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }


    // helper method to transtion scene to demo end ( will go to second level when more levels made )

    void DemoScreen() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
    }
}
