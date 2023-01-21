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

    [SerializeField]
    private GameObject[] drops;


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
        int randomIndex = Random.Range(0,drops.Length);
        enemyIsDead = true;
        base.Die();
        PlayParticleEffects(deathEffect);
        Instantiate(drops[randomIndex], new Vector3(transform.position.x, 1.5f, transform.position.z), Quaternion.identity);

    }


}
