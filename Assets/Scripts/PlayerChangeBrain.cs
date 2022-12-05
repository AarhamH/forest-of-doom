using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerChangeBrain : PlayerController
{
    [Header("Character Traversal Settings")]
    public GameObject character;
    public List<GameObject> characterList;
    public int whichCharacter;

    [Header("Cameras")]
    public CinemachineVirtualCamera thirdPersonCamera;
    public CinemachineVirtualCamera aimCamera;
    public CinemachineVirtualCamera deathCamera;

    PlayerController playerController;

    private void Start() 
    {
        playerController = GetComponent<PlayerController>();
        playerController.PlayerControllerInstance();
        PlayerControllerInstance();
        if(character == null && characterList.Count >=1){
            character = characterList[0];
        }
        Swap();
    }

     void Update() {
        HandleCharacterChange();
    }

    private void HandleCharacterChange() {
        if (changePlayerLeft.triggered)
        {
            Debug.Log("ChangingCharacterLeft");

            if (whichCharacter == 0) {
                whichCharacter = characterList.Count - 1;
            }

            else {
                whichCharacter -= 1;
            }
            Swap();
        }

        if (changePlayerRight.triggered) {
            Debug.Log("ChangingCharacterRight");

            if (whichCharacter == characterList.Count - 1) {
                whichCharacter = 0;
            }

            else {
                whichCharacter += 1;
            }
            Swap();
        }
    }

    public void Swap() {
        character = characterList[whichCharacter];
        OnOffComponents(character,true);

        for (int i = 0; i < characterList.Count; i++)
        {
            if(characterList[i] != character){
                OnOffComponents(characterList[i],false);
            }
        }
    }

    private void OnOffComponents(GameObject character, bool isCharacter) {

        character.GetComponent<Movement>().enabled = isCharacter;
        character.GetComponent<Aim>().enabled = isCharacter;
        character.GetComponent<Gravity>().enabled = !isCharacter;
        character.GetComponent<Animator>().enabled = isCharacter;

        if(character.tag == "Thrower") {
            character.GetComponent<Throwing>().enabled = isCharacter;
        }

        if(character.tag == "Melee"){
            character.GetComponent<SwordAttack>().enabled = isCharacter;
        }

        if(isCharacter) {
            thirdPersonCamera.LookAt = character.transform;
            thirdPersonCamera.Follow = character.transform;

            aimCamera.LookAt = character.transform;
            aimCamera.Follow = character.transform;

            deathCamera.LookAt = character.transform;
            deathCamera.Follow = character.transform;
        }
    }
}
