using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

    public CinemachineVirtualCamera deathCamera;
    public CinemachineVirtualCamera thirdPersonCamera;
    public CinemachineVirtualCamera aimCamera;

    static public bool playerIsDead;
    public bool dead;

    private void Awake() {
        InitializeHealth(maxHealth);
        animationController = GetComponent<AnimationController>();
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
        dead = true;
        base.Die();
        animationController.ExecuteAnimation("Die");

        if(deathEffect != null){
            PlayParticleEffects(deathEffect);
        }
        
    }

    


}
