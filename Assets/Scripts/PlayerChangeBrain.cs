using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerChangeBrain : PlayerController
{
    public GameObject character;
    public List<GameObject> characterList;
    public int whichCharacter;
    public CinemachineVirtualCamera thirdPersonCamera;
    public CinemachineVirtualCamera aimCamera;
    public CinemachineVirtualCamera deathCamera;

    private void Start() 
    {
        PlayerControllerInstance();
        if(character == null && characterList.Count >=1){
            character = characterList[0];
        }
        Swap();
    }

     void Update()
    {
        HandleCharacterChange();
    }

    private void HandleCharacterChange()
    {
        if (changePlayerLeft.triggered)
        {
            Debug.Log("ChangingCharacterLeft");

            if (whichCharacter == 0)
            {
                whichCharacter = characterList.Count - 1;
            }

            else
            {
                whichCharacter -= 1;
            }
            Swap();
        }

        if (changePlayerRight.triggered)
        {
            Debug.Log("ChangingCharacterRight");

            if (whichCharacter == characterList.Count - 1)
            {
                whichCharacter = 0;
            }

            else
            {
                whichCharacter += 1;
            }
            Swap();
        }
    }

    public void Swap()
    {
        character = characterList[whichCharacter];
        character.GetComponent<Movement>().enabled = true;
        character.GetComponent<Aim>().enabled = true;
        character.GetComponent<Gravity>().enabled = false;

        if(character.name == "Bomber"){
            character.GetComponent<Throwing>().enabled = true;
        }
        if(character.name == "SwordsMan"){
            character.GetComponent<SwordAttack>().enabled = true;
        }

        thirdPersonCamera.LookAt = character.transform;
        thirdPersonCamera.Follow = character.transform;

        aimCamera.LookAt = character.transform;
        aimCamera.Follow = character.transform;

        deathCamera.LookAt = character.transform;
        deathCamera.Follow = character.transform;


        for (int i = 0; i < characterList.Count; i++)
        {
            if(characterList[i] != character){
                characterList[i].GetComponent<Movement>().enabled = false;
                characterList[i].GetComponent<Aim>().enabled = false;
                characterList[i].GetComponent<Gravity>().enabled = true;

                if(characterList[i].GetComponent<Throwing>() != null){
                    characterList[i].GetComponent<Throwing>().enabled = false;
                }
                if(characterList[i].GetComponent<SwordAttack>() != null){
                    characterList[i].GetComponent<SwordAttack>().enabled = false;
                }
            }
        }
    }

}
