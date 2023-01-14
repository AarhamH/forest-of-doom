using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    [Header("Health")]
    [SerializeField]
    private float maxHealth = 50; 
      
    [Header("Particle Effects")]
    [SerializeField]
    private GameObject splatterEffect;
    [SerializeField]
    private GameObject deathEffect;

    [SerializeField]
    private Healthbar healthbar;

    public bool enemyIsDead;


    private void Awake() {
        InitializeHealth(maxHealth);
        healthbar.UpdateHealthBar(maxHealth,currentHealth);
    }

    public override void TakeDamage(float damage)
    {
        if(splatterEffect != null){
            PlayParticleEffects(splatterEffect);
        }
        base.TakeDamage(damage);        

        healthbar.UpdateHealthBar(maxHealth,currentHealth);

        Debug.Log(transform.name + "has" + currentHealth + "health");
    }

    public override void Die()
    {
        enemyIsDead = true;
        base.Die();
        PlayParticleEffects(deathEffect);

    }


}
