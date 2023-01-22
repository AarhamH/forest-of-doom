using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Interactable
{
    public PlayerChangeBrain playerChangeBrain;
    public GameObject bomb;

    private void Start() {
    }

    public override string GetDescription()
    {
        if(playerChangeBrain.character.tag != "Healer") {
            return "This gem requires <i><b>magic</b></i> to be activated!";
        }
        return "[T] Activate Gem";
    }

    public override void Interact()
    {
        if(playerChangeBrain.character.tag != "Healer") {
            Instantiate(bomb,this.transform.position,Quaternion.identity);
        }
        else {
            Destroy(this.transform.GetChild(1).gameObject);
            this.GetComponent<BoxCollider>().enabled = false;
        }


    }

}
