using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    protected CharacterController controller;
    protected Transform cameraTransform;
    protected PlayerInput playerInput;
    protected InputAction moveAction;
    protected InputAction jumpAction;
    protected InputAction shootAction;

    protected void PlayerControllerInstance()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];

        Cursor.lockState = CursorLockMode.Locked;

        controller.detectCollisions = false;
    }

    protected void DisableInput(){
        moveAction.Disable();
        jumpAction.Disable();
        shootAction.Disable();
    }

}