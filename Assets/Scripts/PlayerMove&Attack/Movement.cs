using UnityEngine.InputSystem;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Quantity Settings")]
    public float playerSpeed = 7.0f;
    public float jumpHeight = 1.5f;
    public float gravityValue = -9.81f;
    public float rotationSpeed = 5.0f;

    [Header("Movement Misc. Settings")]
    public Vector3 playerVelocity;
    public bool groundedPlayer;

    [Header("Movement Animation Settings")]
    Vector2 currentAnimationBlendVector;
    Vector2 animationVelocity;

    [Header("Player Stats Instance")]
    PlayerStats playerStats;

    [Header("Animation/Player Controller Instance")]
    AnimationController animationController;
    PlayerController playerController;


    private void Awake() {
        // intialize player controller, player stats and animation controls
        playerController = GetComponent<PlayerController>();
        playerController.PlayerControllerInstance();

        playerStats = GetComponent<PlayerStats>();

        animationController = GetComponent<AnimationController>();
    }

    private void Update() {
        Move();
        Jump();
        Look();

        if(PlayerStats.playerIsDead){
            playerController.DisableInput();
        }
    }

    private void Move() {
        groundedPlayer = playerController.controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -3.6f;
        }

        // reads the player input and translates the object respectively around x and z axis
        // animation blend vectors play animation given any combination of WASD pressed and idle
        Vector2 input = playerController.moveAction.ReadValue<Vector2>();
        currentAnimationBlendVector = Vector2.SmoothDamp(currentAnimationBlendVector, input, ref animationVelocity, animationController.animationSmoothTime);
        Vector3 move = new Vector3(currentAnimationBlendVector.x, 0, currentAnimationBlendVector.y);
        move = move.x * playerController.cameraTransform.right.normalized + move.z * playerController.cameraTransform.forward.normalized;
        move.y = 0f;


        playerController.controller.Move(move * Time.deltaTime * playerSpeed);

        //Animator settings
        animationController.WalkAnimation("MoveX","MoveZ",currentAnimationBlendVector.x,currentAnimationBlendVector.y);
    }
    
    private void Jump() {
        // Changes the height position of the player when jumped and decrements y velocity by gravity factor
        if (playerController.jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            animationController.ExecuteAnimation("Jump");
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        playerController.controller.Move(playerVelocity * Time.deltaTime);
    }

    private void Look() {
        //Rotate towards camera direction
        float targetAngle = playerController.cameraTransform.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }



}
