using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    PlayerController playerController;
    public GameObject dialogueBox;

    float timer = 0f;
    float endTimer = 20f;

    bool started;


    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerController.PlayerControllerInstance();
        dialogueBox.SetActive(false);
        sentences = new Queue<string>();       
    }

    private void Update() {
        if(playerController.test.triggered) {
            Debug.Log("Yasss");
        }
        if(started) {
            timer += Time.deltaTime;
            if(timer >= endTimer) {
                timer = 0f;
                DisplayNextSentence();
            }
            else if(playerController.test.triggered) {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(Dialogue dialogue) {
        dialogueBox.SetActive(true);
        started = true;
        Debug.Log("Start Conversation" + dialogue.names);
        nameText.text = dialogue.names;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
            DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if(sentences.Count == 0) {
            EndDialogue();
            return;
        }
            string sentence = sentences.Dequeue();
            dialogueText.text = sentence;
            Debug.Log(sentence);
        



    }

    void EndDialogue() {
        dialogueBox.SetActive(false);
        Debug.Log("Dialog Ended");
    }
}
