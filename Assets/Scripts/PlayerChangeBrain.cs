using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeBrain : PlayerController
{
    public GameObject character;
    public List<GameObject> characterList;
    public int whichCharacter;

    private void Start() {
        PlayerControllerInstance();
        if(character == null && characterList.Count >=1){
            character = characterList[0];
        }
        Swap();
    }

     void Update() {
        if(changePlayerLeft.triggered){
            Debug.Log("ChangingCharacterLeft");

            if(whichCharacter == 0){
                whichCharacter = characterList.Count - 1;
            }

            else{
                whichCharacter -= 1;
            }
            Swap();
        }

        if(changePlayerRight.triggered){
            Debug.Log("ChangingCharacterRight");

            if(whichCharacter == characterList.Count - 1){
                whichCharacter = 0;
            }

            else{
                whichCharacter += 1;
            }
            Swap();
        }
    }

    public void Swap(){
        character = characterList[whichCharacter];
        character.GetComponent<Movement>().enabled = true;
        character.GetComponent<Aim>().enabled = true;


        //TODO: give thirdperson,aim,death cameras respective tags and remove them in the forloop
        //      make a completely new script for gravity, otherwise character will float
        for (int i = 0; i < characterList.Count; i++)
        {
            if(characterList[i] != character){
                characterList[i].GetComponent<Movement>().enabled = false;
                characterList[i].GetComponent<Aim>().enabled = false;

            }
        }
    }

}
