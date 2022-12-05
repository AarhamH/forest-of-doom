using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class is used for disabled players, so that they don't float in the air (pretty jank ik)
public class Gravity : Movement
{
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerController.PlayerControllerInstance();
    }

    // Update is called once per frame
    void Update()
    {
        // this code is also in Jump() in the Movement class, only called when the character is disabled
        playerVelocity.y += gravityValue * Time.deltaTime;
        playerController.controller.Move(playerVelocity * Time.deltaTime);

    }
}
