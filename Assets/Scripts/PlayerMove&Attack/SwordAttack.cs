using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : PlayerController
{
    AnimationController animationController;
    
    int attackAnimation;
    public int damage = 20;
    public bool readyToAttack;
    public bool collisionDisable;

    public Transform attackPoint;
    public float attackPointRange = 1.5f;

    Collider[] targets;

    public float knockBackForce;


    private void Awake() {
        PlayerControllerInstance();
        animationController = GetComponent<AnimationController>();
        animationController.AnimationPlayerInstance();
        attackAnimation = Animator.StringToHash("Attack2");

        collisionDisable = true;
        readyToAttack = true;
    }

    private void Update() {
        if(shootAction.triggered && readyToAttack){
            Attack();
            DoDamage();
        }
    }


    public void Attack(){
        readyToAttack = false;
        animationController.animator.CrossFade(attackAnimation,animationController.animationPlayTransition);
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
