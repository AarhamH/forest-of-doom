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
    public GameObject icons;

    public string fullText;
    public string currentText = "";

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
            if(timer >= endTimer || playerController.test.triggered) {
                timer = 0f;
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
        AudioManager.Instance.PlayEffect("DialogueSound");
        if(sentences.Count == 0) {
            EndDialogue();
            return;
        }
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(ShowText(sentence));
            Debug.Log(sentence);
    }

    void EndDialogue() {
        dialogueBox.SetActive(false);
        Debug.Log("Dialog Ended");
    }

    IEnumerator ShowText(string parseText) {
        currentText = "";
        for(int i=0;i<parseText.Length;i++) {
            yield return new WaitForSeconds(0.005f); 
            currentText += parseText[i];
            dialogueText.text = currentText;
        }
    }

    public void SetActiveIcon(string tag) {
        for(int i=0;i<icons.transform.childCount;i++) {
            if(icons.transform.GetChild(i).tag != tag) {
                icons.transform.GetChild(i).gameObject.SetActive(false);
            }
            if(icons.transform.GetChild(i).tag == tag) {
                icons.transform.GetChild(i).gameObject.SetActive(true);
            }

        }
    }



}
