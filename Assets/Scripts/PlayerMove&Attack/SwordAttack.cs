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

    int powerSwing;


    private void Awake() {
        playerController = GetComponent<PlayerController>();
        playerController.PlayerControllerInstance();
        animationController = GetComponent<AnimationController>();

        collisionDisable = true;
        readyToAttack = true;
        powerSwing = 0;
    }

    private void Update() {
        if(playerController.shootAction.triggered && readyToAttack){
            Attack();
            DoDamage();
        }
    }


    public void Attack(){
        powerSwing++;
        if(powerSwing == 3) {
            animationController.ExecuteAnimation("AttackSpin");
            powerSwing = 0;
        }   
        else {
            animationController.ExecuteAnimation("Attack2");
        }
        Invoke(nameof(ResetAttack), 1f);
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

    private void ResetAttack(){
        readyToAttack = true;
        collisionDisable = true;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackPointRange);
    }

}
