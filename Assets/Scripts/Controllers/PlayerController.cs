using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]

// parent class instead of general class due to the input system inheritance
public class PlayerController : MonoBehaviour
{
    [Header("Controller")]
    protected CharacterController controller;

    [Header("Camera Reference")]
    protected Transform cameraTransform;

    [Header("Player Inputs")]
    protected PlayerInput playerInput;
    protected InputAction moveAction;
    protected InputAction jumpAction;
    protected InputAction shootAction;
    protected InputAction changePlayerRight;
    protected InputAction changePlayerLeft;

    protected void PlayerControllerInstance()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];
        changePlayerRight = playerInput.actions["ChangePlayerRight"];
        changePlayerLeft = playerInput.actions["ChangePlayerLeft"];


        Cursor.lockState = CursorLockMode.Locked;

        controller.detectCollisions = false;
    }

    public void DisableInput(){
        this.moveAction.Disable();
        this.jumpAction.Disable();
        this.shootAction.Disable();
    }

}