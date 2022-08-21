using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    [Header("Health")]
    [SerializeField]
    private int maxHealth = 50; 
      
    [Header("Particle Effects")]
    [SerializeField]
    private GameObject splatterEffect;

    [SerializeField]
    private Rigidbody rb;

    static public bool enemyIsDead;


    private void Awake() {
        InitializeHealth(maxHealth);
        rb = GetComponent<Rigidbody>();
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
        enemyIsDead = true;
        base.Die();

    }
}
