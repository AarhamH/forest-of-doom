using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crucifix : Interactable
{
    public GameObject characterBrain;
    public PlayerChangeBrain playerChangeBrain;
    public Dialogue dialogue;
    public DialogueManager manager;
    
    int index;


    private void Start() {
    }

    public override string GetDescription()
    {
        return "Hold [T] Rescue the prisoner";
    }

    public override void Interact()
    {
        Talk();
        ActivateChild(index);
        Destroy(this.gameObject);

    }

    private void ActivateChild(int index) {
        for(int i=0;i<characterBrain.transform.childCount;i++) {
            if(characterBrain.transform.GetChild(i).tag == this.tag) {
                index = i;
            }
        }
        characterBrain.transform.GetChild(index).gameObject.SetActive(true);
        GameObject newGuy = characterBrain.transform.GetChild(index).gameObject;
        newGuy.transform.position = this.transform.position;
        playerChangeBrain.characterList.Add(newGuy);
        playerChangeBrain.Swap();
    }

    private void Talk() {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        FindObjectOfType<DialogueManager>().SetActiveIcon(this.tag);
    }
}
