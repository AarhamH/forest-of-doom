using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    
    [Header("Attack Stats")]
    [SerializeField]
    private int damage = 20;
    [SerializeField]
    private float knockBackForce = 20f;

    [Header("Attack Stats")]
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float attackPointRange = 1.5f;

    [Header("Shield")]
    [SerializeField]
    private GameObject shield;

    [Header("Particle Effects")]
    [SerializeField]
    private GameObject swordSwing;

    [Header("Classes")]
    PlayerController playerController;
    AnimationController animationController;
    Movement movement;

    [Header("Attack Conditions")]
    bool readyToAttack;
    bool collisionDisable;
    bool attackDisabled;
    int powerSwing;

    [Header("Targets")]
    Collider[] targets;


    /*
        Awake Function: enables input controls, animations and Movement class and sets initial conditions 
    */
    private void Awake() {
        playerController = GetComponent<PlayerController>();
        playerController.PlayerControllerInstance();
        animationController = GetComponent<AnimationController>();
        movement = GetComponent<Movement>();

        // set readyToAttack and collisionDisable to true so the character can attack and disables shield
        // game object
        ResetAttack();
        powerSwing = 0;
        shield.SetActive(false);
    }


    /*
        Update Function: If the player hits MB1 and is not already attacking, Attack() and DoDamage
                         executed
                         Shield() also called but only executed given conditions in Shield()
    */
    private void Update() {
        if(playerController.shootAction.triggered && readyToAttack && !attackDisabled){
            Attack();
            DoDamage(attackPoint,attackPointRange);
        }

        Shield();
    }


    /*
        Input: n/A
    
        Functionality: Driver for the sword attack called in Update()
                       Essentially modifies readyToAttack bool and plays sword attack animation
                       Also handles a special spin attack when player attacks 3 times 
    */
    public void Attack(){
        readyToAttack = false;
        powerSwing++;
        if(powerSwing == 3) {
            animationController.ExecuteAnimation("AttackSpin");
            Instantiate(swordSwing, transform.position,Quaternion.identity);
            powerSwing = 0;
            DoDamage(this.transform,5f);
        }   
        else {
            animationController.ExecuteAnimation("Attack2");
        }
        Invoke(nameof(ResetAttack), 0.75f);
    }


    /*
        Input: pos(Transform) => the position where the collision sphere is going to start
               range(float) => the range of the collision sphere
    
        Functionality: creates an overlap sphere which detects gameObjects and checks if they
                       are enemies; if they are get access to EnemyStats class and inflict damage
                       Also plays sword sound from singleton AudioManager class

        Important Functions Docs:
                OverlapSphere(): https://docs.unity3d.com/ScriptReference/Physics.OverlapSphere.html
                AddForceAtPosition(): https://docs.unity3d.com/ScriptReference/Rigidbody.AddForceAtPosition.html
    */
    public void DoDamage(Transform pos, float range){
        targets = Physics.OverlapSphere(pos.position,range);
        
        foreach(Collider target in targets){
            if(target.tag == "Enemy"){
                target.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
            
                AudioManager.Instance.PlayEffect("NormalAttackSword");
            }

            // inflict knockback
            if(target.GetComponent<Rigidbody>() != null){
                Vector3 objectPos = target.transform.position;
                Vector3 forceDirection = (objectPos - attackPoint.position);

                target.GetComponent<Rigidbody>().AddForceAtPosition(forceDirection*knockBackForce + Vector3.up*knockBackForce,
                                                                    attackPoint.position + new Vector3(0,-10f,0),
                                                                    ForceMode.Impulse);
            }
        }
    }


    /*
        Input: n/A

        Functionality: iff the conditions met, activate shield gameObject
    */
    private void Shield(){
        if(SwitchVCam.aimCalled && !PlayerStats.playerIsDead){
            movement.playerSpeed = 1.5f;
            attackDisabled = true;
            if(attackDisabled) {
                animationController.animator.Play("Shield");
            }
            shield.SetActive(true);
        }
        else{
            movement.playerSpeed = 7f;
            attackDisabled = false;
            shield.SetActive(false);
        }
    }


    /*
        Input: n/A
    
        Functionality: helper function used in Attack()
                       resets the attack and colider bool to original settings to allow
                       player to attack again
    */
    private void ResetAttack(){
        readyToAttack = true;
        collisionDisable = true;
    }


    /*
        Input: n/A
    
        Functionality: debug tool that draws attack spheres
    */
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackPointRange);
        Gizmos.DrawWireSphere(this.transform.position, 4f);
    }
}
