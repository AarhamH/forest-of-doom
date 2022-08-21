using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    [Header("Health")]
    [SerializeField]
    private int maxHealth = 100; 
      
    [Header("Particle Effects")]
    [SerializeField]
    private GameObject splatterEffect;
    [SerializeField]
    private GameObject deathEffect;

    AnimationController animationController;
    int dieAnimation;

    static public bool playerIsDead;

    private void Awake() {
        InitializeHealth(maxHealth);

        animationController = GetComponent<AnimationController>();
        animationController.AnimationPlayerInstance();
        dieAnimation = Animator.StringToHash("Die");
    }

    public override void TakeDamage(int damage)
    {
        if(splatterEffect != null){
            PlayParticleEffects(splatterEffect);
        }
        
        base.TakeDamage(damage);        

        Debug.Log(transform.name + "has" + currentHealth + "health");
    }

    public override void Die()
    {
        playerIsDead = true;
        base.Die();
        animationController.animator.CrossFade(dieAnimation, animationController.animationPlayTransition);

        if(deathEffect != null){
            PlayParticleEffects(deathEffect);
        }
    }

    


}
