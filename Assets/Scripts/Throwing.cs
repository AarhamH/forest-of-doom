using UnityEngine;
using UnityEngine.InputSystem;

public class Throwing : PlayerController
{
    [Header("Throwing References")]
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private Transform pointOfAttack;
    [SerializeField]
    private GameObject throwableObject;
    [SerializeField]
    private GameObject bombInHand;

    [Header("Throw Settings")]
    [SerializeField]
    private int totalThrows = 50;
    [SerializeField]
    private float throwCooldown = 0.3f;
    [SerializeField]
    private float throwUpwardForce;
    [SerializeField]
    private float throwAnimationDelay = 0.3f;
    private float throwForce;

    int throwAnimation;
    bool readyToThrow;
    AnimationController animationController;


    // Awake() [executes before Start() and Update()]
    //     :=> Initializes all required components used before run time
    private void Awake() 
    {
        cam = GameObject.Find("Main Camera").transform;
        // Initializes PlayerControllerInstance; method inherited 
        // from PlayerController.cs
        //      --> gets Unity Input System components for Move, Jump, Shoot
        PlayerControllerInstance();
    
        // Initializes AnimationPlayerInstance; method inherited 
        // from PlayerController.cs
        //      --> gets Animator component for Animations to be active
        animationController = GetComponent<AnimationController>();
        animationController.AnimationPlayerInstance();
    
        // Initalizes throwAnimation as the Throw animation from Animator Controller
        throwAnimation = Animator.StringToHash("Throw");
    }


    // Start() [exectutes before Update()]
    //      :=> Initializes all required components at run time
    private void Start()
    {
        // Initializes readyToThrow as true so the Player can throw bombs when 
        // the game is launched 
        readyToThrow = true;
    }

    // Update() []
    //      :=> Runs code in function every frame the game runs
    private void Update() 
    {
        // Calls Throw() given the following conditions:
        //      --> the shoot button is pressed (1)
        //      --> the character has bombs to throw (2)
        //      --> the character is ready to throw (3)
        BombVisibleController();

        if(shootAction.triggered && totalThrows > 0 && readyToThrow){
            Throw();
        }
    }

    // Throw() [method called every frame in Update() above]
    //      :=> The actual function that handles Player throws
    private void Throw()
    {
        // Calls PlayThrowAnimation(), which plays throw animation and blends 
        // with other animations 
        PlayThrowAnimation();
        FastThrow();


        // Calls ThrowMechanics() at delayed time (details of ThrowMechanics() below)
        //      --> uses Invoke() on ThrowMechanics with delayed time of 
        //          "throwAnimationDelay"
        Invoke(nameof(ThrowMechanics), throwAnimationDelay);

        // readyToThrow is set to false after so Player can't spam
        readyToThrow = false;

        // Calls ResetThrow() at a delayed time (details of ResetThrow() below)
        // uses Invoke() on ResetThrow() with delayed time of "throwCooldown"
        Invoke(nameof(ResetThrow), throwCooldown);

        // decrements totalThrows everytime Throw() is called until 0 throws
        totalThrows--;
        }

    // PlayThrowAnimation() []
    //      :=> Handles the throwing animation when Throw() is called above
    private void PlayThrowAnimation()
    {
        // Plays throwAnimation in the animator
        // Utilize CrossFade method, which fades the throwAnimation when over
        animationController.animator.CrossFade(throwAnimation, animationController.animationPlayTransition);   

    }

    // ResetThrow() []
    //      :=> Helper function that resets the throw status in Throw() above
    private void ResetThrow()
    {
        // sets readyToThrow = true... that's it.
        readyToThrow = true;
    }

    //ThrowMechanics() []
    //      :=> Handles physics of the prefab bomb when Throw() is called above
    private void ThrowMechanics()
    {
        // Instantiate the projectile to be thrown at a given position, as well 
        // as the projectile's RigidBody
        GameObject projectile = Instantiate(throwableObject, 
                                            pointOfAttack.position, 
                                            cam.rotation);
                                            
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 forceDirection = cam.transform.forward;
        RaycastHit hit;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity)){
            forceDirection = (hit.point - pointOfAttack.position).normalized;
        }

        // Adds the projectile's force using the camera's transformation and the
        // upward transformation, applying throwFroce and throwUpwardForce 
        // quantities respectively
        Vector3 forceToAdd = (forceDirection * throwForce) + 
                             (transform.up * throwUpwardForce); 

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
    }

    private void FastThrow(){
        if(SwitchVCam.aimCalled){
            throwForce = 100f;
        }
        else{
            throwForce = 30f;
        }
    }

    private void BombVisibleController(){
        if(!readyToThrow || totalThrows <= 0){
            Invoke(nameof(GetRidOfBomb), throwAnimationDelay);
        }
        else{
            GetBackBomb();
        }
    }

    private void GetRidOfBomb()  {bombInHand.SetActive(false);}
    private void GetBackBomb()  {bombInHand.SetActive(true);}
}
