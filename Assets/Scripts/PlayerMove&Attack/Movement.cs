using UnityEngine;

/*
    Movement Class: Attached to all controllable characters to handle their movement

    Necessary Components: n/A
*/
public class Movement : MonoBehaviour
{
    [Header("Movement Quantity Settings")]
    public float playerSpeed = 7.0f;
    public float gravityValue = -9.81f;
    [SerializeField]
    private float jumpHeight = 1.5f;
    [SerializeField]
    private float rotationSpeed = 5.0f;

    [Header("Movement Misc. Settings")]
    public Vector3 playerVelocity;
    private bool groundedPlayer;

    [Header("Movement Animation Settings")]
    private Vector2 currentAnimationBlendVector;
    private Vector2 animationVelocity;

    [Header("Player Stats Instance")]
    private PlayerStats playerStats;

    [Header("Animation/Player Controller Instance")]
    private AnimationController animationController;
    private PlayerController playerController;


    /*
        Awake Function: enables input controls, animations and stats class 
    */
    private void Awake() 
    {
        // intialize player controller, player stats and animation controls
        playerController = GetComponent<PlayerController>();
        playerController.PlayerControllerInstance();

        playerStats = GetComponent<PlayerStats>();

        animationController = GetComponent<AnimationController>();
    }

    /*
        Update Function: - Move(), Jump(), and Look() called every frame but executed based on 
                           condtions inside those functions
                         - If the player is dead, input is disable, so the object cant move when 
                           dead
    */    
    private void Update() 
    {
        Move();
        Jump();
        Look();

        if(PlayerStats.playerIsDead)
        {
            playerController.DisableInput();
        }
    }

    /*
        Input: n/A
    
        Functionality: - Main function which allows character move around x-z axis
                       - Reads input and creates a Vector3 'move'
                       - Blend vector created which plays animation based on combination of inputs
                       - Move() method takes 'move' vector, speed and real time to translate the
                         character

        Called In: Update()

        Notable Functions Docs:
         - SmoothDamp(): https://docs.unity3d.com/ScriptReference/Vector3.SmoothDamp.html
         - Controller Move(): https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
    */
    private void Move() {
        groundedPlayer = playerController.controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -3.6f;
        }

        // animation blend vectors play animation given any combination of WASD pressed and idle
        Vector2 input = playerController.moveAction.ReadValue<Vector2>();
        currentAnimationBlendVector = Vector2.SmoothDamp(currentAnimationBlendVector, input, 
                                                         ref animationVelocity, 
                                                         animationController.animationSmoothTime);

        Vector3 move = new Vector3(currentAnimationBlendVector.x, 0, currentAnimationBlendVector.y);
        move = move.x * playerController.cameraTransform.right.normalized + move.z * playerController.cameraTransform.forward.normalized;
        move.y = 0f;

        // the driver which translates the character based on the move vector and player speed
        playerController.controller.Move(move * Time.deltaTime * playerSpeed);

        //Animator settings
        animationController.WalkAnimation("MoveX","MoveZ",currentAnimationBlendVector.x,
                                           currentAnimationBlendVector.y);
    }
    

    /*
        Input: n/A
    
        Functionality: - Method to allow the character to jump
                       - If jump button pressed (SPACE), y component of velocity vector modified and
                         gravity brings the velocity down to normal

        Called In: Update()
    */
    private void Jump() {
        // Changes the height position of the player when jumped and decrements y 
        // velocity by gravity factor
        if (playerController.jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            animationController.ExecuteAnimation("Jump");
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        playerController.controller.Move(playerVelocity * Time.deltaTime);
    }


    /*
        Input: n/A
    
        Functionality: - Method to change the camera based on where the character looks

        Called In: Update()

        Notable Functions Docs:
         - Euler(): https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html
         - Lerp(): https://docs.unity3d.com/ScriptReference/Quaternion.Lerp.html
    */
    private void Look() 
    {
        //Rotate towards camera direction
        float targetAngle = playerController.cameraTransform.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 
                                             rotationSpeed * Time.deltaTime);
    }



}
