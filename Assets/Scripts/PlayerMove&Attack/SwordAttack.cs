using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    AnimationController animationController;
    
    public int damage = 20;
    public bool readyToAttack;
    public bool collisionDisable;

    public Transform attackPoint;
    public float attackPointRange = 1.5f;

    Collider[] targets;

    public float knockBackForce;

    PlayerController playerController;

    public GameObject shield;

    int powerSwing;
    bool attackDisabled;

    Movement movement;


    private void Awake() {
        playerController = GetComponent<PlayerController>();
        playerController.PlayerControllerInstance();
        animationController = GetComponent<AnimationController>();
        movement = GetComponent<Movement>();

        collisionDisable = true;
        readyToAttack = true;
        powerSwing = 0;
        shield.SetActive(false);
    }

    private void Update() {
        if(playerController.shootAction.triggered && readyToAttack && !attackDisabled){
            Attack();
            DoDamage();
        }

        Shield();
    }


    public void Attack(){
        readyToAttack = false;
        powerSwing++;
        if(powerSwing == 3) {
            animationController.ExecuteAnimation("AttackSpin");
            powerSwing = 0;
        }   
        else {
            animationController.ExecuteAnimation("Attack2");
        }
        Invoke(nameof(ResetAttack), 0.75f);
    }

    public void DoDamage(){
        targets = Physics.OverlapSphere(attackPoint.position,attackPointRange);
        foreach(Collider target in targets){
            if(target.tag == "Enemy"){
                target.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
            }

            if(target.GetComponent<Rigidbody>() != null){
                Vector3 objectPos = target.transform.position;
                Vector3 forceDirection = (objectPos - attackPoint.position);

                target.GetComponent<Rigidbody>().AddForceAtPosition(forceDirection*knockBackForce + Vector3.up*knockBackForce,attackPoint.position + new Vector3(0,-10f,0),ForceMode.Impulse);
            }
        }
    }

    private void Shield(){
        // if the player aims (Mouse 2), the bomb is faster
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

    private void ResetAttack(){
        readyToAttack = true;
        collisionDisable = true;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackPointRange);
    }
    

}
