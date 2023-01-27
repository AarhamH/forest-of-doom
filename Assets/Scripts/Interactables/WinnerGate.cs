using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerGate : Interactable
{
    public GameObject characterBrain;
    public PlayerChangeBrain playerChangeBrain;
    
    int index;
    public bool win;

    private void Start() {
        win = false;
    }

    public override string GetDescription()
    {
        return "Hole [T] to exit the forest!";
    }

    public override void Interact()
    {
        win = true;
        this.GetComponent<BoxCollider>().enabled = false;
    }




}
