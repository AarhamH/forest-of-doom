using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]

// parent class instead of general class due to the input system inheritance
public class PlayerController : MonoBehaviour
{
    [Header("Controller")]
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


        Cursor.lockState = CursorLockMode.Locked;

        controller.detectCollisions = false;
    }

    public void DisableInput(){
        this.moveAction.Disable();
        this.jumpAction.Disable();
        this.shootAction.Disable();
    }

}