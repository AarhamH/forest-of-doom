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


    private void Awake() 
    {
        cam = GameObject.Find("Main Camera").transform;

        //initialize player controller and animation controls;
        PlayerControllerInstance();
    
        animationController = GetComponent<AnimationController>();
        animationController.AnimationPlayerInstance();
        throwAnimation = Animator.StringToHash("Throw");
    }


    private void Start()
    {
        // player can throw when the game starts
        readyToThrow = true;
    }


    private void Update() 
    {
        BombVisibleController();

        if(shootAction.triggered && totalThrows > 0 && readyToThrow){
            Throw();
        }
    }


    private void Throw()
    {
        FastThrow();

        // throw is delayed so it matches the throw animation
        Invoke(nameof(ThrowMechanics), throwAnimationDelay);
        animationController.animator.CrossFade(throwAnimation, animationController.animationPlayTransition);   

        // readyToThrow is set to false after so Player can't spam
        readyToThrow = false;

        Invoke(nameof(ResetThrow), throwCooldown);

        totalThrows--;
        }


    private void ResetThrow()
    {
        readyToThrow = true;
    }


    private void ThrowMechanics()
    {
        // instantiate object at the point of attack, which is the bomb itself on the hand
        GameObject projectile = Instantiate(throwableObject, 
                                            pointOfAttack.position, 
                                            cam.rotation);
                                            
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // use raycast to make player throws more accurate
        Vector3 forceDirection = cam.transform.forward;
        RaycastHit hit;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity)){
            forceDirection = (hit.point - pointOfAttack.position).normalized;
        }

        // apply force to the projectile
        Vector3 forceToAdd = (forceDirection * throwForce) + 
                             (transform.up * throwUpwardForce); 

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
    }


    private void FastThrow(){
        // if the player aims (Mouse 2), the bomb is faster
        if(SwitchVCam.aimCalled){
            throwForce = 100f;
        }
        else{
            throwForce = 30f;
        }
    }


    private void BombVisibleController(){
        // bomb disappears when the bomb is thrown
        // cosmetic feature
        if(!readyToThrow || totalThrows <= 0){
            Invoke(nameof(GetRidOfBomb), throwAnimationDelay);
        }
        else{
            GetBackBomb();
        }
    }

    // helper functions
    private void GetRidOfBomb()  {bombInHand.SetActive(false);}
    private void GetBackBomb()  {bombInHand.SetActive(true);}

}
