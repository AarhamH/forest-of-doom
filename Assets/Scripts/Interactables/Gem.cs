using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Interactable
{
    public PlayerChangeBrain playerChangeBrain;
    public GameObject bomb;
    public GateOpen gateOpen;
    
    [SerializeField]
    private GameObject activateEffect;
    int counter;

    private void Start() {
        counter = 0;
    }

    public override string GetDescription()
    {
        if(playerChangeBrain.character.tag != "Healer") {
            return "This gem requires <i><b><u>magic</u></b></i> to be activated!";
        }
        else if(playerChangeBrain.character.gameObject.GetComponent<Healing>().currentMana- 50f <= 0f) {
            return "Not enough mana to activate gem";
        }
        return "Hold [T] Activate Gem";
    }

    public override void Interact()
    {
        if(playerChangeBrain.character.tag != "Healer") {
            Instantiate(bomb,this.transform.position,Quaternion.identity);
        }
        else {
            if(playerChangeBrain.character.gameObject.GetComponent<Healing>().currentMana- 50f >= 0f) {
                Destroy(this.transform.GetChild(1).gameObject);
                gateOpen.counter+= 1;
                if(gateOpen.counter  >= 9) {
                    gateOpen.OpenGate();
                }
                playerChangeBrain.character.gameObject.GetComponent<Healing>().currentMana -= 50f;
                AudioManager.Instance.PlayEffect("HealImpact");
                Instantiate(activateEffect,new Vector3(this.transform.position.x,1.3f,this.transform.position.z),Quaternion.identity);
                this.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

}
