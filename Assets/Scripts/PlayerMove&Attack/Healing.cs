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
    private float maxMana = 200f;
    private float currentMana;

    [SerializeField]
    private float throwCooldown = 1f;
    [SerializeField]
    private float throwUpwardForce;
    [SerializeField]
    private float throwForce = 25f;

    int throwAnimation;
    bool readyToThrow;
    AnimationController animationController;
    PlayerController playerController;
    Movement movement;
    ManaBar manaBar;

    bool healingState;

    private void Awake() 
    {
        cam = GameObject.Find("Main Camera").transform;
        playerController = GetComponent<PlayerController>();
        playerController.PlayerControllerInstance();

        movement = GetComponent<Movement>();
    
        animationController = GetComponent<AnimationController>();

        currentMana = maxMana;
        manaBar = GetComponent<ManaBar>();

    }


    private void Start()
    {
        // player can throw when the game starts
        readyToThrow = true;
    }


    private void Update() 
    {
        if(playerController.shootAction.triggered && currentMana > 0 && readyToThrow){
            Throw();
        }
        FastThrow();
        manaBar.UpdateHealth(maxMana,currentMana);
    }


    private void Throw()
    {
        // throw is delayed so it matches the throw animation
        if(healingState) {
            float subMana = 80f;
            if(currentMana - subMana <= 0) {

            }
            else {
                ThrowMechanics(healingObject);
                currentMana -= subMana;
                readyToThrow = false;               
            }
        }

        else {
            float subMana = 50f;
            if(currentMana - subMana <= 0) {

            }
            else {
                ThrowMechanics(throwableObject);
                currentMana -= subMana;
                readyToThrow = false;
            }

        }

        animationController.ExecuteAnimation("Throw");  
        Invoke(nameof(ResetThrow), throwCooldown);

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
