using UnityEngine.InputSystem;
using UnityEngine;

public class Movement : PlayerController
{
    [Header("Movement Quantity Settings")]
    [SerializeField]
    private float playerSpeed = 4.0f;
    [SerializeField]
    private float jumpHeight = 2.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 5.0f;

    [Header("Movement Misc. Settings")]
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [Header("Movement Animation Settings")]
    private Vector2 currentAnimationBlendVector;
    private Vector2 animationVelocity;
    int moveXAnimationParamID;
    int moveZAnimationParamID;
    int jumpAnimation;

    [Header("Player Stats Instance")]
    private PlayerStats playerStats;

    [Header("Animation Controller Instance")]
    AnimationController animationController;


    private void Awake(){
        // intialize player controller, player stats and animation controls
        PlayerControllerInstance();
        playerStats = GetComponent<PlayerStats>();

        animationController = GetComponent<AnimationController>();
        animationController.AnimationPlayerInstance();

        moveXAnimationParamID = Animator.StringToHash("MoveX");
        moveZAnimationParamID = Animator.StringToHash("MoveZ");
        jumpAnimation = Animator.StringToHash("Jump");
    }

    private void Update()
    {
        Move();
        Jump();
        Look();

        if(PlayerStats.playerIsDead){
            DisableInput();
        }
    }

    private void Move()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -3.6f;
        }

        // reads the player input and transforms the object respectively around x and z axis
        // animation blend vectors play animation given any combination of WASD pressed and idle
        Vector2 input = moveAction.ReadValue<Vector2>();
        currentAnimationBlendVector = Vector2.SmoothDamp(currentAnimationBlendVector, input, ref animationVelocity, animationController.animationSmoothTime);
        Vector3 move = new Vector3(currentAnimationBlendVector.x, 0, currentAnimationBlendVector.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;

        controller.Move(move * Time.deltaTime * playerSpeed);

        //Animator settings
        animationController.animator.SetFloat(moveXAnimationParamID, currentAnimationBlendVector.x);
        animationController.animator.SetFloat(moveZAnimationParamID, currentAnimationBlendVector.y);
    }
    
    private void Jump()
    {
        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            animationController.animator.CrossFade(jumpAnimation, animationController.animationPlayTransition);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void Look()
    {
        //Rotate towards camera direction
        float targetAngle = cameraTransform.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

}
