using UnityEngine;
using UnityEngine.InputSystem;

/*
    Healing Class: Attached to the Mage character which allows character to heal and mark enemies
    Necessary Components: Attack Point(Transform) => dictates where the projectiles will be loaded
*/
public class Healing : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField]
    private float throwCooldown = 1f;
    [SerializeField]
    private float throwForce = 25f;

    [Header("Mana Settings")]
    public float maxMana = 200f;
    public float currentMana;

    [Header("Throwing References")]
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private Transform pointOfAttack;

    [Header("Projectile References")]
    [SerializeField]
    private GameObject throwableObject;
    [SerializeField]
    private GameObject healingObject;

    [Header("Animation Settings")]
    AnimationController animationController;
    int throwAnimation;

    [Header("Input Classes")]
    PlayerController playerController;
    Movement movement;

    [Header("UI Classes")]
    AmmoBar manaBar;

    [Header("State Bools")]
    bool readyToCast;
    bool healingState;

    /*
        Awake Function: enables input controls, camera, animations and Movement class and sets 
        initial conditions 
    */
    private void Awake() 
    {
        playerController = GetComponent<PlayerController>();
        playerController.PlayerControllerInstance();

        movement = GetComponent<Movement>();
    
        animationController = GetComponent<AnimationController>();

        cam = GameObject.Find("Main Camera").transform;

        manaBar = GetComponent<AmmoBar>();

        currentMana = maxMana;
        readyToCast = true;
    }


    /*
        Update Function: - If M1 pressed and conditions met, Mage can shoot
                         - HealSet() called but only executed when conditions within function are met
                         - Mana bar in the UI is updated every frame
    */
    private void Update() 
    {
        if(playerController.shootAction.triggered && currentMana > 0 && readyToCast){
            Cast();
        }
        HealSet();
        manaBar.UpdateHealth(maxMana,currentMana);
    }


    /*
        Input: n/A

        Output: n/A

        Functionality: - Driver for the projectile shooting called in Update()
                       - Depending on healingState (depends on HealSet()), a light or healing object
                         will be instantiated and mana will be reducted
                       - Animations and sounds based on the projectile will be loaded as well
        
        Called In: Update()
    */
    private void Cast()
    {
        // healing portion
        if(healingState) {
            float subMana = 80f;
            if(currentMana - subMana <= 0) {

            }
            else {
                AudioManager.Instance.PlayEffect("HealShoot");
                CastMechanics(healingObject);
                currentMana -= subMana;
                readyToCast = false;               
            }
        }

        // light portion
        else {
            float subMana = 50f;
            if(currentMana - subMana <= 0) {

            }
            else {
                AudioManager.Instance.PlayEffect("LightShoot");
                CastMechanics(throwableObject);
                currentMana -= subMana;
                readyToCast = false;
            }

        }

        // play animation and call Reset() after a delayed time
        animationController.ExecuteAnimation("Throw");  
        Invoke(nameof(ResetThrow), throwCooldown);
    }


    /*
        Input: n/A

        Output: n/A

        Functionality: - Simple method which sets state to true to allow the mage to attack again
                       - Method called in Cast() in invoked time; prevents player from spamming

        Called In: Cast()
    */  
    private void ResetThrow()
    {
        readyToCast = true;
    }


    /*
        Input: GameObject magic => the projectile that will be instantiate (heal orb or light)

        Output: n/A

        Functionality: - Main mechanism that allows projectiles to shoot forward
                       - Instantiates the projectile from argument at the attackPoint, which follows 
                         a RayCast from the crosshair
                       - force physics applied to the rigid body and object destroyed when hit

        Called In: Cast()

        Notable Function Docs:
        - RayCast(): https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
    */  
    private void CastMechanics(GameObject magic)
    {
        // instantiate object at the point of attack, which is the bomb itself on the hand
        GameObject projectile = Instantiate(magic, pointOfAttack.position, cam.rotation);                        
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // use raycast to make player throws more accurate
        Vector3 forceDirection = cam.transform.forward;
        RaycastHit hit;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity)) {
            forceDirection = (hit.point - pointOfAttack.position).normalized;
        }

        // apply force to the projectile
        Vector3 forceToAdd = (forceDirection * throwForce); 
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
    }


    /*
        Input: n/A
        Output: n/A
        Functionality: - Function which allows the mage character to switch between light state and
                         heal state 
                       - if healing, the playerSpeed is slower (obtained from Movement Class)

        Called In: Update()
    */ 
    private void HealSet()
    {
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
