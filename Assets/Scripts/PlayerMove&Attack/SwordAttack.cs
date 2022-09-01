using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : PlayerController
{
    AnimationController animationController;
    
    int attackAnimation;

    public bool readyToAttack;
    public bool isAttacking;

    private void Awake() {
        PlayerControllerInstance();
        animationController = GetComponent<AnimationController>();
        animationController.AnimationPlayerInstance();
        attackAnimation = Animator.StringToHash("Attack2");
    }
    private void Start() {
        readyToAttack = true;
        isAttacking = false;
    }
    private void Update() {
        if(shootAction.triggered && readyToAttack){
            Attack();
        }
    }
    public void Attack(){
        readyToAttack = false;
        isAttacking = true;
        animationController.animator.CrossFade(attackAnimation,animationController.animationPlayTransition);
        Invoke(nameof(ResetAttack), 1f);
        Invoke(nameof(NotAttacking),1f);
    }

    private void ResetAttack(){
        readyToAttack = true;
    }
    private void NotAttacking(){
        isAttacking = false;
    }
}
