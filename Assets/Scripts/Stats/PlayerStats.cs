using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerStats : CharacterStats
{
    [Header("Health")]
    [SerializeField]
    public float maxHealth = 100; 
      
    [Header("Particle Effects")]
    [SerializeField]
    private GameObject splatterEffect;
    [SerializeField]
    private GameObject deathEffect;

    AnimationController animationController;
    MainHealth mainHealth;
    int dieAnimation;

    public CinemachineVirtualCamera deathCamera;
    public CinemachineVirtualCamera thirdPersonCamera;
    public CinemachineVirtualCamera aimCamera;

    static public bool playerIsDead;
    public bool dead;

    private void Awake() {
        InitializeHealth(maxHealth);
        animationController = GetComponent<AnimationController>();
        mainHealth = GetComponent<MainHealth>();
    }

    private void Update() {
        mainHealth.UpdateHealth(maxHealth,currentHealth);
    }

    public override void TakeDamage(float damage)
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
