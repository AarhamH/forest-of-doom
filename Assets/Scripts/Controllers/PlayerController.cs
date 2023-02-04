using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]

/*
    PlayerController class: Main Input System class for the characters
    Necessary Components: n/A (All instantiated automatically)
*/
public class PlayerController : MonoBehaviour
{
    [Header("Controllers")]
    public CharacterController controller;

    [Header("Camera Reference")]
    public Transform cameraTransform;

    [Header("Player Inputs")]
    public PlayerInput playerInput;
    public InputAction moveAction;
    public InputAction jumpAction;
    public InputAction shootAction;
    public InputAction changePlayerRight;
    public InputAction changePlayerLeft;
    public InputAction test;
    public InputAction pause;


    /*
        Input: n/A

        Output: n/A

        Functionality: - Instantiates the cameras, controller and key bindings
                       - Locks the cursor to not show during playtime
        
        Called In: Awake() of other classes
    */
    public void PlayerControllerInstance()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];
        changePlayerRight = playerInput.actions["ChangePlayerRight"];
        changePlayerLeft = playerInput.actions["ChangePlayerLeft"];
        test = playerInput.actions["Test"];
        pause = playerInput.actions["Pause"];


        Cursor.lockState = CursorLockMode.Locked;

        controller.detectCollisions = false;
    }


    /*
        Input: n/A

        Output: n/A

        Functionality: - Disables the controls of the character

    */
    public void DisableInput(){
        this.moveAction.Disable();
        this.jumpAction.Disable();
        this.shootAction.Disable();
    }

}