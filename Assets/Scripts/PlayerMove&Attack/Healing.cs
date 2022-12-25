using UnityEngine;
using UnityEngine.InputSystem;

public class Healing : MonoBehaviour
{
    [Header("Throwing References")]
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private Transform pointOfAttack;
    [SerializeField]
    private GameObject throwableObject;
    [SerializeField]
    private GameObject healingObject;
    
    [Header("Throw Settings")]
    [SerializeField]
    private int totalThrows = 50;
    [SerializeField]
    private float throwCooldown = 0.001f;
    [SerializeField]
    private float throwUpwardForce;
    [SerializeField]
    private float throwAnimationDelay = 0.3f;
    private float throwForce = 25f;

    int throwAnimation;
    bool readyToThrow;
    AnimationController animationController;
    PlayerController playerController;
    Movement movement;

    bool healingState;

    private void Awake() 
    {
        cam = GameObject.Find("Main Camera").transform;
        playerController = GetComponent<PlayerController>();
        playerController.PlayerControllerInstance();

        movement = GetComponent<Movement>();
    
        animationController = GetComponent<AnimationController>();
    }


    private void Start()
    {
        // player can throw when the game starts
        readyToThrow = true;
    }


    private void Update() 
    {
        if(playerController.shootAction.triggered && totalThrows > 0 && readyToThrow){
            Throw();
        }
        FastThrow();
    }


    private void Throw()
    {
        // throw is delayed so it matches the throw animation
        if(healingState) {
            ThrowMechanics(healingObject);
        }

        else {
            ThrowMechanics(throwableObject);
        }
        animationController.ExecuteAnimation("Throw");  


        totalThrows--;
        }


    private void ResetThrow()
    {
        readyToThrow = true;
    }


    private void ThrowMechanics(GameObject magic)
    {
        // instantiate object at the point of attack, which is the bomb itself on the hand
        GameObject projectile = Instantiate(magic, 
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
        Vector3 forceToAdd = (forceDirection * throwForce); 

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
    }

    private void FastThrow(){
        // if the player aims (Mouse 2), the bomb is faster
        if(SwitchVCam.aimCalled){
            healingState = true;
            movement.playerSpeed = 1.5f;

        }
        else{
            healingState = false;
            movement.playerSpeed = 7f;
        }
    }

}
