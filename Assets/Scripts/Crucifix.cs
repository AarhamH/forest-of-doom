using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crucifix : Interactable
{
    public GameObject characterBrain;
    public PlayerChangeBrain playerChangeBrain;
    private void Start() {

    }

    public override string GetDescription()
    {
        return "Talk to the fukin prisioner";
    }

    public override void Interact()
    {
        characterBrain.transform.GetChild(4).gameObject.SetActive(true);
        GameObject newGuy = characterBrain.transform.GetChild(4).gameObject;
        newGuy.transform.position = this.transform.position;
        playerChangeBrain.characterList.Add(newGuy);
        playerChangeBrain.character = playerChangeBrain.characterList[0];
        playerChangeBrain.Swap();
        Destroy(this.gameObject);

    }
}
