using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : PlayerController
{
    AnimationController animationController;
    
    int attackAnimation;

    private void Awake() {
        PlayerControllerInstance();
        animationController = GetComponent<AnimationController>();
        animationController.AnimationPlayerInstance();
        attackAnimation = Animator.StringToHash("Attack2");
    }
    private void Update() {
        if(shootAction.triggered){
            Attack();
        }
    }
    public void Attack(){
        animationController.animator.CrossFade(attackAnimation,animationController.animationPlayTransition);
    }
}
