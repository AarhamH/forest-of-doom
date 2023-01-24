using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpen : Interactable
{
    public int counter = 0;

    public override string GetDescription()
    {
        return "9 Gems are around the land. Activate them all to open this gate...";
    }

    private void Awake() {
        counter = 0;
    }

    public void OpenGate() {
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(true);
    }

    public override void Interact(){}
}
