using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public GameObject instructionUI;
    public int instructionType;
    public GameObject characterBrain;
    PlayerChangeBrain playerChangeBrain;

    float time = 0f;
    float max = 5f;


    private void Awake() {
        characterBrain = GameObject.Find("CharacterBrain");
        playerChangeBrain = characterBrain.GetComponent<PlayerChangeBrain>();
    }
    private void Update() {

        if(instructionUI.transform.GetChild(instructionType).gameObject.activeSelf)
        {
            time += Time.deltaTime;
            if(time >= max)
            {
                time = 0f;
                instructionUI.transform.GetChild(instructionType).gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("whatIsPlayer") && instructionType != 2) {
            instructionUI.transform.GetChild(instructionType).gameObject.SetActive(true);
            this.GetComponent<BoxCollider>().enabled = false;
        }

        else if(other.gameObject.layer == LayerMask.NameToLayer("whatIsPlayer") && instructionType == 2 && playerChangeBrain.characterList.Count >1) {
            instructionUI.transform.GetChild(instructionType).gameObject.SetActive(true);
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }

}
